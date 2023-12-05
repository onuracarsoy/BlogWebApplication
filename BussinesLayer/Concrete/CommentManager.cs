﻿using BussinesLayer.Abstract;
using DataAccesLayer.Abstract;
using DataAccesLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
	public class CommentManager : ICommentService
	{
		ICommentDal _commentDal;

		public CommentManager(ICommentDal commentDal)
		{
			_commentDal = commentDal;
		}

		public void CommentAdd(Comment comment)
		{

				_commentDal.Insert(comment);
		

		}

		public List<Comment> GetCommentWithBlog()
		{
			return _commentDal.GetListWithBlog();
		}

		public List<Comment> GetList(int id)
		{
		 return _commentDal.GetListAll(x=>x.BlogID == id);

		}
		
		}

	}
