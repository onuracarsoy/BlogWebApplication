﻿using BussinesLayer.Abstract;
using DataAccesLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
	public class ContactManager : IContactService
	{

		IContactDal _contactDal;

		public ContactManager(IContactDal contactDal)
		{
			_contactDal = contactDal;
		}

		public void ContactAdd(Contact contact)
		{
			_contactDal.Insert(contact);
		}
	}
}
