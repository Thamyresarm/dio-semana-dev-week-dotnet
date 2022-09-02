using System.Collections.Generic;

namespace src.Models;

 public class Person{

    public Person()
{
    this.name = "template";
    this.age = 0;
    this.contracts = new List<Contract>();
    this.active = true;
}
  public Person(string Name, int Age, string Cpf)
{
    this.name = Name;
    this.age = Age;
    this.cpf = Cpf;
    this.contracts = new List<Contract>();
    this.active = true;
}

    public int id { get; set; }
    public string name{get; set;}
    public int age { get; set; }
    public string? cpf { get; set; }
    public bool active { get; set; }
    public List<Contract> contracts{get; set;}

 }