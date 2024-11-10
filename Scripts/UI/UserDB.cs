using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Security.Cryptography;
using UnityEngine;
using Mono.Data.Sqlite;

public class UserDB : MonoBehaviour
{

    const int saltSize = 16;
    const int hashSize = 20;
    const int iterations = 50000;

    private string dbName = "URI=file:users.db";
    public static UserDB db; // global singleton instance of the DB

    void Start()
    {
        //dummy data for demo
        // AddUser("kelly", "123", "admin", highscore: 300);
        // AddUser("Luke", "314", "admin", highscore: 0);
        // AddUser("grrrr", "101", "user", highscore: 500);
        // AddUser("tryhard", "333", "user", highscore: 150000);
        // AddUser("bubbles", "222", "user", highscore: 100500);
        // AddUser("bruh", "222", highscore: 12450);
    }

    void Awake()
    {
        if (db != null && db != this)
        {
            Destroy(gameObject);
        } else 
        {
            db = this;
        }

        // preserve the DB instance when new scenes are loaded
        DontDestroyOnLoad(gameObject);
    }

    public void CreateDB() 
    {
        // create the db connection
        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // create the users table if it doesn't already exist
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS users (
                    uid INTEGER PRIMARY KEY AUTOINCREMENT,
                    username VARCHAR(20) NOT NULL UNIQUE, 
                    password VARCHAR(20) NOT NULL, 
                    role VARCHAR(5) NOT NULL, 
                    highscore INTEGER, 
                    currscore INTEGER, 
                    currlives INTEGER, 
                    currhealth INTEGER, 
                    testtubes INTEGER, 
                    lastwave INTEGER
                );";
                command.ExecuteNonQuery();
                
            }

            conn.Close();
        }

    }

    // add user with given info to DB. returns true if successful.
    public bool AddUser(string username, string password, string role = "user", int highscore = 0, int currscore = 0, int currlives = 3, int currhealth = 100, int testtubes = 3, int lastwave = 0)
    {
        bool result = true;
        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // create new user with given info (default values for info not provided)
                command.CommandText = @"INSERT INTO users (
                username, password, role, highscore, currscore, currlives, currhealth, testtubes, lastwave) 
                VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9);";

                command.Parameters.AddWithValue("@param1", username);
                command.Parameters.AddWithValue("@param2", HashPassword(password)); // hash the password before storing
                command.Parameters.AddWithValue("@param3", role);
                command.Parameters.AddWithValue("@param4", highscore);
                command.Parameters.AddWithValue("@param5", currscore);
                command.Parameters.AddWithValue("@param6", currlives);
                command.Parameters.AddWithValue("@param7", currhealth);
                command.Parameters.AddWithValue("@param8", testtubes);
                command.Parameters.AddWithValue("@param9", lastwave);

                try
                {
                    command.ExecuteNonQuery();
                } catch (SqliteException se)
                {
                    result = false;
                }

            }

            conn.Close();
        }

        return result;
    }

    // returns the save data of the user with the given UID as an array
    public int[] GetUserSaveData(int uid)
    { 
        int[] saveData = new int[6];

        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // find the user with the given username
                command.CommandText = @"SELECT highscore, currscore, currlives, currhealth, testtubes, lastwave FROM users WHERE uid == @param1 LIMIT 1;";
                command.Parameters.AddWithValue("@param1", uid);
                
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        // read each value from query return into the corresponding slot in saveData array
                        for (int i = 0; i < 6; i++)
                        {
                            saveData[i] = reader.GetInt32(i);
                        }
                    }

                    reader.Close();
                }
            }

            conn.Close();
        }

        return saveData;
    }

    // updates the given user's save data
    public void SetUserSaveData(int[] saveData, int uid)
    {
        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // find the user with the given uid and update their save data
                command.CommandText = @"UPDATE users SET 
                highscore = @param1, 
                currscore = @param2, 
                currlives = @param3, 
                currhealth = @param4, 
                testtubes = @param5, 
                lastwave = @param6
                WHERE uid = @param7;";

                // insert each piece of save data as its corresponding query parameter
                for (int i = 1; i <= 6; i++) 
                {
                    string param = "@param" + i;
                    command.Parameters.AddWithValue(param, saveData[i - 1]);
                }

                command.Parameters.AddWithValue("@param7", uid);
                command.ExecuteNonQuery();
            }

            conn.Close();
        }
    }

    // determines if user with the given username exists in DB
    // returns the uid of the existing user, 0 if user doesn't exist
    public int UserExists(string username)
    {
        int uid = 0;
        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                // get the uid of the user with the given username (case insensitive)
                command.CommandText = @"SELECT uid FROM users WHERE username = @param1 COLLATE NOCASE;";
                command.Parameters.AddWithValue("@param1", username);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uid = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }
            conn.Close();
        }

        return uid;

    }

    // hashes a given string password
    string HashPassword(string password)
    {

        // create the salt
        byte[] salt = new byte[saltSize];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            // fill the salt array with a random value
            rng.GetBytes(salt);
        }

        // create hash value
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash = pbkdf2.GetBytes(hashSize);

        // combine salt and pw bytes for later
        var hashBytes = new byte[saltSize + hashSize];
        Array.Copy(salt, 0, hashBytes, 0, saltSize);
        Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

        // turn salt+hash into string to store in DB
        string savedPWHash = Convert.ToBase64String(hashBytes);

        return savedPWHash;

    }

    // given a username and password, checks if password is correct for this user.
    public bool PasswordCorrect(string username, string password)
    {
        string pwDB = "";
        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // get the password for this user from DB
                command.CommandText = @"SELECT password FROM users WHERE username = @param1 COLLATE NOCASE;";
                command.Parameters.AddWithValue("@param1", username);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pwDB = reader.GetString(0);
                    }

                    reader.Close();
                }
            }
            conn.Close();
        }

        // compare the stored pw to the inputted pw
        // extract the bytes
        byte[] hashBytes = Convert.FromBase64String(pwDB);
        // get the salt
        byte[] salt = new byte[saltSize];
        Array.Copy(hashBytes, 0, salt, 0, saltSize);
        // compute the hash on the inputted password
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        byte[] hash = pbkdf2.GetBytes(hashSize);
        for (int i = 0; i < hashSize; i++)
        {
            if (hashBytes[i + saltSize] != hash[i])
                {
                     return false;           
                }
        }

        return true;
    }

    public string GetScoreboardData() 
    {
        string scoreboardStats = "";

        using (var conn = new SqliteConnection(dbName))
        {
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                // get the top 10 users
                command.CommandText = @"SELECT username, highscore FROM users ORDER BY highscore DESC LIMIT 10;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // add the next user and their score to the scoreboard text
                        scoreboardStats += reader["username"] + "\t\t" + reader["highscore"] + "\n";
                    }

                    reader.Close();
                }
            }

            conn.Close();
        }

        return scoreboardStats;
    }
}
