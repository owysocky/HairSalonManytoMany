using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=olha_wysocky_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
      Specialty.ClearAll();
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "Test Stylist";
      Stylist newStylist = new Stylist(name);
      string result = newStylist.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      string name01 = "Work";
      string name02 = "School";
      Stylist newStylist1 = new Stylist(name01);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };
      List<Stylist> result = Stylist.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Name");
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyClientList_ClientList()
    {
      string name = "Work";
      Stylist newStylist = new Stylist(name);
      List<Client> newList = new List<Client> { };
      List<Client> result = newStylist.GetClients();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_List()
    {
      int result = Stylist.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      Stylist firstStylist = new Stylist("Name");
      Stylist secondStylist = new Stylist("Name");
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("Name");
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      Stylist testStylist = new Stylist("Name");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      Stylist testStylist = new Stylist("Name");
      testStylist.Save();
      Client firstClient = new Client(testStylist.GetId(), "Name1", 1);
      firstClient.Save();
      Client secondClient = new Client(testStylist.GetId(), "Name2", 1);
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      string name = "Nate";
      Stylist newStylist = new Stylist(name);
      string updatedName = "Olya";
      newStylist.SetName(updatedName);
      string result = newStylist.GetName();
      Assert.AreEqual(updatedName, result);
    }

  }
}
