using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Custom;
using Microsoft.EntityFrameworkCore;
using BlazorCrud.Shared;
using BlazorCrud.Server.Models;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
         
    }
}
