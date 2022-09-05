using Microsoft.AspNetCore.Mvc;
using src.Models;

using src.Persintence;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase{

    private DatabaseContext _context { get; set; }

    public PersonController(DatabaseContext context){
        this._context = context;
    }

[HttpGet]
    public ActionResult<List<Person>> Get(){

       // Person pessoa = new Person("Thamyres", 30, "11111111111");
       // Contract newContract = new Contract("abc123", 50.46);

       // pessoa.contracts.Add(newContract);
        // return person;

        var result = _context.persons.Include(p => p.contracts).ToList();    

        if (!result.Any()) return NoContent();

        return Ok(result);         
    }

[HttpPost]
    public ActionResult<Person> Post(Person person){

        try{
        _context.persons.Add(person);
        _context.SaveChanges();

        } catch {
            return BadRequest(new{
            msg = "Ocorreu um erro ao tentar criar a Pessoa",
            status = HttpStatusCode.BadRequest                
            });
        }

        return Created("Criado ", person);
    }

    [HttpPut("{id}")]
    public ActionResult<Object> Update([FromRoute] int id, [FromBody] Person person){

       /* Validação de Id impedindo de atualizar com algum conflito não identificado
       var result = _context.persons.SingleOrDefault(e => e.id == id);

        if(result is null){
            return NotFound(new {
                msg = "Registro não encontrado",
                status = HttpStatusCode.NotFound
            });   
        }*/

        try{
            _context.persons.Update(person);
            _context.SaveChanges();

      }catch(System.Exception){
            return BadRequest(new {
            msg = "Ocorreu um erro ao enviar solicitação de atualização do id " + id,
            status = HttpStatusCode.BadRequest
            });
        }
  
        return Ok(new {
            msg = "Dados do id " + id + " atualizados",
            status = HttpStatusCode.OK
        });

    }

    [HttpDelete("{id}")]
    public ActionResult<Object> Delete([FromRoute] int id){

        var result = _context.persons.SingleOrDefault(e => e.id == id );

        if(result is null) return BadRequest(new {
            msg = "Conteúdo inexistente, solicitação inválida",
            status = HttpStatusCode.BadRequest
        });

        _context.persons.Remove(result);
        _context.SaveChanges();

        return Ok(new {
            msg = "deletado pessoa de Id " + id,
            status = HttpStatusCode.OK
        });
    }

}