using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class InterestsContext : IDb<Interest, string>
    {
        private readonly PeopleDbContext dbContext;

        public InterestsContext(PeopleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Interest item)
        {
            try
            {
                dbContext.Interests.Add(item);
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
                Interest interestFromDb = Read(key);

                if (interestFromDb != null)
                {
                    dbContext.Interests.Remove(interestFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Interest with that key does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Interest Read(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Interest> query = dbContext.Interests;
                if (useNavigationalProperties)
                {
                    query = query.Include(i => i.Users).Include(i => i.Regions);
                }
                return query.FirstOrDefault(i => i.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Interest> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Interest> query = dbContext.Interests;

                if (useNavigationalProperties)
                {
                    query = query.Include(i => i.Users).Include(i => i.Regions);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Interest item, bool useNavigationalProperties = false)
        {
            try
            {
                Interest interestFromDb = Read(item.Id, useNavigationalProperties);

                if (interestFromDb == null)
                {
                    Create(item);
                    return;
                }

                interestFromDb.Id = item.Id;
                interestFromDb.Name = item.Name;

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

                    interestFromDb.Users = users;

                    List<Region> regions = new List<Region>();
                    foreach (Region r in item.Regions)
                    {
                        Region regionFromDb = dbContext.Regions.Find(r.Id);

                        if (regionFromDb != null)
                        {
                            regions.Add(regionFromDb);
                        }
                        else
                        {
                            regions.Add(r);
                        }

                    }

                    interestFromDb.Regions = regions;
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
