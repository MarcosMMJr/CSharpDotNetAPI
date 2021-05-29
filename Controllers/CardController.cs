using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vaivoa.Data;
using vaivoa.Models;

namespace vaivoa.Controllers
{

  [ApiController]
  [Route("v1/cards")]
  public class CardController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<CardInfo>>> Get([FromServices] DataContext context)
    {
      var cardNumber = await context.CardInfos.Include(x => x.AccountInfo).ToListAsync();
      return cardNumber;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<CardInfo>> GetById([FromServices] DataContext context, int id)
    {
      var cardType = await context.CardInfos.Include(x => x.AccountInfo)
        .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
      return cardType;
    }

    [HttpGet]
    [Route("/accounts/{id:int}")]
    public async Task<ActionResult<List<CardInfo>>> GetByAccount([FromServices] DataContext context, int id)
    {
      var cardNumber = await context.CardInfos.Include(x => x.CardNumber).AsNoTracking()
        .Where(x => x.AccountId == id & x.CardType == "Virtual").ToListAsync();
      return cardNumber;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<CardInfo>> Post([FromServices] DataContext context, [FromBody] CardInfo model)
    {
      if (ModelState.IsValid)
      { 
        context.CardInfos.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      else 
      {
        return BadRequest(ModelState);
      }
    }
  }
}