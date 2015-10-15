using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;
using VdoValley.Interfaces;

namespace VdoValley.Infrastructure.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        VdoValleyContext db = new VdoValleyContext();

        public int Save(Tag Tag)
        {
            if (Tag.TagId == 0)
            {
                db.Tags.Add(Tag);
            }
            else
            {
                db.Entry(Tag).State = EntityState.Modified;
            }
            return db.SaveChanges();
        }

        public void Remove(int TagId)
        {
            Tag Tag = db.Tags.Find(TagId);
            db.Tags.Remove(Tag);
            db.SaveChanges();
        }

        public List<Tag> GetTags()
        {
            return db.Tags.ToList<Tag>();
        }

        public Tag FindById(int TagId)
        {
            var Tag = (from v in db.Tags where v.TagId == TagId select v).FirstOrDefault();
            return Tag;
        }

        public List<Tag> GetSuggestedTagsForTitle(string Title, string RootUrl)
        {
            var Tokens = Title.Split(" ".ToCharArray()).ToList();
            List<Tag> RawTags = new List<Tag>();
            List<Tag> SuggestedTags = new List<Tag>();

            foreach (var Token in Tokens)
            {
                for (int j = Tokens.IndexOf(Token); j < Tokens.Count(); j++)
                {
                    var SubToken = string.Empty;
                    for (var k = Tokens.IndexOf(Token); k <= j; k++)
                    {
                        SubToken = SubToken + " " + Tokens[k];
                    }

                    if (!string.IsNullOrWhiteSpace(SubToken))
                    {
                        // remove special characters at start of string
                        SubToken = SubToken.Replace("^([^a-zA-Z0-9])*", string.Empty);
                        // remove special characters at the end of string
                        SubToken = SubToken.Replace("([^a-zA-Z0-9])*$", string.Empty);
                        // remove 's|' at the end of string
                        SubToken = SubToken.Replace("('s|')$", string.Empty);

                        var Tag = new Tag
                        {
                            Name = SubToken.Trim()
                        };

                        RawTags.Add(Tag);
                    }
                }
            }

            //string serviceUrl = string.Format(RootUrl + "Tags/GetTags");
            string serviceUrl = string.Format("http://vdovalley.com/Tags/GetTags");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.Method = "POST";
            request.Accept = "application/json; charset=UTF-8";
            request.ContentType = "application/json; charset=UTF-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(RawTags));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                SuggestedTags = JsonConvert.DeserializeObject<List<Tag>>(responseText);
            }

            return SuggestedTags;
        }
    }
}
