﻿using DataAccesLayer.Abstract;
using DataAccesLayer.Repositroies;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.EntityFramework
{
    public class EFMessageRepository : GenericRepository<Message>,IMessageDal
    {
    }
}
