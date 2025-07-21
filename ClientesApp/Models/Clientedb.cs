using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace AppCliente.Models
{
    public class ClienteDataAccess
    {
        private readonly string _connectionString;

        public ClienteDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cliente> CargaLista()
        {
            var clientes = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT cliente_id, cliente_nombre, cliente_email FROM Clientes";
                SqlCommand gen = new SqlCommand(query, conn);
                SqlDataReader data = gen.ExecuteReader();
                while (data.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id = (int)data["cliente_id"],
                        Nombre = data["cliente_nombre"].ToString(),
                        Email = data["cliente_email"].ToString()
                    });
                }
            }
            return clientes;
        }

        public string Agrega(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Email))
                return "Nombre y Email son obligatorios.";

            int cliente_idNew = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT MAX(cliente_id) + 1 AS newId FROM Clientes";
                SqlCommand gen = new SqlCommand(query, conn);
                SqlDataReader data = gen.ExecuteReader();
                while (data.Read())
                {
                    cliente_idNew = (int)data["newId"];
                    if (cliente_idNew == 0) cliente_idNew = 1;
                }
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string queryIns = "INSERT INTO Clientes (cliente_id, cliente_nombre, cliente_email) VALUES (@Id, @Nombre, @Email)";
                SqlCommand genIns = new SqlCommand(queryIns, conn);
                genIns.Parameters.AddWithValue("@Id", cliente_idNew);
                genIns.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                genIns.Parameters.AddWithValue("@Email", cliente.Email);
                try
                {
                    genIns.ExecuteNonQuery();

                    return "";
                }
                catch (SqlException ex)
                {
                    return $"Error al insertar: {ex.Message}";
                }
            }
        }

        public string Elimina(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Clientes WHERE cliente_id = @Id";
                SqlCommand gen = new SqlCommand(query, conn);
                gen.Parameters.AddWithValue("@Id", id);
                try
                {
                    gen.ExecuteNonQuery();
                    return "Cliente eliminado con éxito.";
                }
                catch (SqlException ex)
                {
                    return $"Error al eliminar: {ex.Message}";
                }
            }
        }
    }
}