using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      List<Specialty> allSpecialties = Specialty.GetAll();
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      model.Add("stylist", selectedStylist);
      model.Add("stylistSpecialties", stylistSpecialties);
      model.Add("clients", stylistClients);
      model.Add("allSpecialties", allSpecialties);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName, int clientPhone)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(stylistId);
      Client newClient = new Client(stylistId, clientName, clientPhone);
      newClient.Save();
      List<Client> stylistClients = selectedStylist.GetClients();
      List<Specialty> allSpecialties = Specialty.GetAll();
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      model.Add("clients", stylistClients);
      model.Add("stylistSpecialties", stylistSpecialties);
      model.Add("stylist", selectedStylist);
      model.Add("allSpecialties", allSpecialties);
      return View("Show", model);
    }

    [HttpPost("/stylists/{stylistId}/clients/delete")]
    public ActionResult DeleteClients(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.DeleteClients();
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpPost("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.DeleteAll();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult AddSpecialty(int stylistId, int specialtyId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(specialtyId);
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpGet("/stylists/{stylistId}/edit")]
    public ActionResult Edit(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/update")]
    public ActionResult Update(int stylistId, string newName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Edit(newName);
      model.Add("stylist", stylist);
      return RedirectToAction("Show",  new { id = stylistId });
    }

  }
}
