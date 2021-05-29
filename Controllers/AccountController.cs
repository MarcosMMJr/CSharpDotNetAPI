using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vaivoa.Data;
using vaivoa.Models;

namespace vaivoa.Controllers
{
  [ApiController]
  [Route("v1/accounts")]
  public class AccountController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<AccountInfo>>> Get([FromServices] DataContext context)
    {
      var infos = await context.AccountInfos.ToListAsync();
      return infos;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<AccountInfo>> Post([FromServices] DataContext context, [FromBody] AccountInfo model)
    {
      if (ModelState.IsValid)
      {
        context.AccountInfos.Add(model);
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