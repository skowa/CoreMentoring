using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Constants;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IMapper _mapper;

        public AdminController(
            IAccountsService accountsService,
            IMapper mapper)
        {
            _accountsService = accountsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = _accountsService.GetUsers();

            return View(_mapper.Map<IEnumerable<UserModel>>(users));
        }
    }
}
