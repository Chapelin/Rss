using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using RssEntity;
using WebAPI.OutputCache;

namespace RssApi.Controllers
{
    public class FaviconController : ApiController
    {
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public HttpResponseMessage GetIcoBySource(string id)
        {
            var fileId = DBManager.Favicon.FindOne(Query<Favicon>.EQ(x => x.SourceId, id));
            if (fileId != null)
            {
                var file = DBManager.GridFS.FindOneById(new ObjectId(fileId.GridFSId));
                var response = new HttpResponseMessage { Content = new StreamContent(file.OpenRead()) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/vnd.microsoft.icon");
                response.Content.Headers.ContentLength = file.Length;
                return response;

            }
            else
            {
                return null;
            }
        }
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public HttpResponseMessage GetFirst()
        {
            var fileId = DBManager.Favicon.FindOne();
            if (fileId != null)
            {
                var file = DBManager.GridFS.FindOneById(new ObjectId(fileId.GridFSId));
                var response = new HttpResponseMessage { Content = new StreamContent(file.OpenRead()) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/vnd.microsoft.icon");
                response.Content.Headers.ContentLength = file.Length;
                return response;

            }
            else
            {
                return null;
            }
        }
    }
}
