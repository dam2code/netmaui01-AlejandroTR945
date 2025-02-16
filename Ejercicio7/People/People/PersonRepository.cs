using SQLite;
using People.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People
{
    public class PersonRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }

        // Variable para la conexión asincrónica
        private SQLiteAsyncConnection conn;

        // Constructor para inicializar la base de datos
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Método asincrónico para inicializar la conexión a la base de datos
        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }

        // Método asincrónico para agregar una nueva persona
        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                // Llamada al método Init() de manera asincrónica
                await Init();

                // Validación básica para asegurar que se ingresa un nombre
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                // Inserción asincrónica de la nueva persona
                result = await conn.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1}]", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        // Método asincrónico para obtener todas las personas
        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                // Llamada al método Init() de manera asincrónica
                await Init();

                // Obtener todas las personas de la base de datos de forma asincrónica
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();  // Devuelve una lista vacía en caso de error
        }
    }
}
