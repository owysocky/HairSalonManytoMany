using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private int _stylist_id;
    private string _name;
    private int _phone;

    public Client(int stylistId, string name, int phone, int id = 0)
    {
      _id = id;
      _stylist_id = stylistId;
      _name = name;
      _phone = phone;
    }

    public int GetId(){ return _id;}
    public int GetStylistId(){return _stylist_id;}
    public string GetName(){return _name;}
    public int GetPhone(){return _phone;}

    public void SetName(string name){_name = name;}

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
       int clientId = rdr.GetInt32(0);
       int clientStylistId = rdr.GetInt32(1);
       string clientName = rdr.GetString(2);
       int clientPhone = rdr.GetInt32(3);
       Client newClient = new Client(clientStylistId, clientName, clientPhone, clientId);
       allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
      return allClients;
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      int clientStylistId = 0;
      string clientName = "";
      int clientPhone = 0;
      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientStylistId = rdr.GetInt32(1);
        clientName = rdr.GetString(2);
        clientPhone = rdr.GetInt32(3);
      }
      Client newClient = new Client(clientStylistId, clientName, clientPhone, clientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newClient;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (stylist_id, name, phone) VALUES (@stylistId, @name, @phone);";
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._stylist_id;
      cmd.Parameters.Add(stylistId);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@phone";
      phone.Value = this._phone;
      cmd.Parameters.Add(phone);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && stylistEquality);
       }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

  }
}
