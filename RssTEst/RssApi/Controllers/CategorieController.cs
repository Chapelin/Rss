﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RssEntity;

namespace RssApi.Controllers
{
    public class CategorieController : ApiController
    {

        public IEnumerable<Categorie> Get()
        {
            return DBManager.Categories.FindAll().OrderBy(x => x.Description);
        }
    }
}