﻿using BussinesLayer.Abstract;
using DataAccesLayer.Abstract;
using DataAccesLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
	public class BlogManager : IBlogService
	{
		IBlogDal _blogDal;

		public BlogManager(IBlogDal blogDal)
		{
			_blogDal = blogDal;
		}

		
		public List<Blog> GetBlogListByWriter(int id)
		{
			return _blogDal.GetListAll(x => x.WriterID == id);
		}



		public List<Blog> GetBlogListWithCategory()
		{
			return _blogDal.GetListWithCategory();
		}



        public List<Blog> GetListWithCategoryByWriterBm(int id)
        {
            return _blogDal.GetListWithCategoryByWriter(id);
        }


        public Blog TGetById(int id)
		{
			return _blogDal.GetById(id);
		}
		public List<Blog> GetByID(int id)
		{
			return _blogDal.GetListAll(x => x.BlogID == id);
		}

		public List<Blog> GetLast3Blog()
		{
			return _blogDal.GetListAll().Take(3).ToList();
		}

		public List<Blog> GetList()
		{
			return _blogDal.GetListAll();
		}

        public void TAdd(Blog t)
        {
			_blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }

        public List<Blog> GetBlogListByWriterBm(int id)
        {
            throw new NotImplementedException();
        }
    }
	}

