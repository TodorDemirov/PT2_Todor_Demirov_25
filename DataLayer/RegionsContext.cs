using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RegionsContext : IDb<Region, string>
    {
        private readonly PeopleDbContext dbContext;

        public RegionsContext(PeopleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Region item)
        {
            try
            {
                dbContext.Regions.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string key)
        {
            try
            {
                Region regionFromDb = Read(key);

                if (regionFromDb != null)
                {
                    dbContext.Regions.Remove(regionFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Region with that key does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Region Read(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Region> query = dbContext.Regions;
                if (useNavigationalProperties)
                {
                    query = query.Include(r => r.Users).Include(r => r.Interests);
                }
                return query.FirstOrDefault(r => r.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Region> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Region> query = dbContext.Regions;

                if (useNavigationalProperties)
                {
                    query = query.Include(r => r.Users).Include(r => r.Interests);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Region item, bool useNavigationalProperties = false)
        {
            try
            {
                Region regionFromDb = Read(item.Id, useNavigationalProperties);

                if (regionFromDb == null)
                {
                    Create(item);
                    return;
                }

                regionFromDb.Id = item.Id;
                regionFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {
                    List<User> users = new List<User>();

                    foreach (User u in item.Users)
                    {
                        User userFromDb = dbContext.Users.Find(u.Id);

                        if (userFromDb != null)
                        {
                            users.Add(userFromDb);
                        }
                        else
                        {
                            users.Add(u);
                        }

                    }

                    regionFromDb.Users = users;

                    List<Interest> interests = new List<Interest>();
                    foreach (Interest i in item.Interests)
                    {
                        Interest interestFromDb = dbContext.Interests.Find(i.Id);

                        if (interestFromDb != null)
                        {
                            interests.Add(interestFromDb);
                        }
                        else
                        {
                            interests.Add(i);
                        }

                    }

                    regionFromDb.Interests = interests;
                }

                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
