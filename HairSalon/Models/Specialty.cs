using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;

    public Stylist(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allSpecialtys;
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM students WHERE id = @id;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@id";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyName = "";
      while(rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyName = rdr.GetString(1);
      }
      Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);
      conn.Close();
      if(conn!=null)
      {
          conn.Dispose();
      }
      return foundSpecialty;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText=@"INSERT INTO specialties (name) VALUES (@name);";
      MySqlParameter specialtyName = new MySqlParameter();
      specialtyName.ParameterName = "@name";
      specialtyName.Value = Name;
      cmd.Parameters.Add(specialtyName);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
          conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @searchId; DELETE FROM stylists_specialties WHERE specialty_id = @search_id";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      List<Stylist> allSpecialtyStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = "SELECT * FROM stylists JOIN stylists_specialties ON (stylists_specialties.stylist_id = stylists.id) JOIN specialties ON (specialties.id = stylists_specialties.specialty_id) WHERE specialties.id = @specialty_id;";
     MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@specialty_id";
      searchId.Value = this._id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        int stylistsName = rdr.GetInt32(1);
        Stylist newStylist= new Stylist(stylistsName, stylistId);
        allSpecialtyStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialtyStylists;
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";
      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);
      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.GetId();
      cmd.Parameters.Add(stylist_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherSpetialty)
    {
        if (!(otherSpetialty is Spetialty))
        {
          return false;
        }
        else
        {
          Spetialty newSpetialty = (Spetialty) otherSpetialty;
          bool idEquality = this.GetId().Equals(newSpetialty.GetId());
          bool nameEquality = this.GetName().Equals(newSpetialty.GetName());
          return (idEquality && nameEquality);
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
      cmd.CommandText = @"DELETE  FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
