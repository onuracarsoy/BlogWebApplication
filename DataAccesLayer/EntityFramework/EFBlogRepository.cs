﻿using DataAccesLayer.Abstract;
using DataAccesLayer.Concrete;
using DataAccesLayer.Repositroies;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.EntityFramework
{
	public class EFBlogRepository : GenericRepository<Blog>, IBlogDal
	{
       

        public List<Blog> GetListWithCategory()
		{
			using (var c = new Context())
			{
				return c.Blogs.Include(x=>x.Category).ToList();
			}
		}

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Where(x=>x.WriterID == id).ToList();
            }
        }
    }
}
