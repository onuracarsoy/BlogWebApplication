﻿using DataAccesLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Abstract
{
    public interface IMessage2Dal: IGenericDal<Message2>
    {
        List<Message2> GetInBoxWitMessageByWriter(int id);
        List<Message2> GetSendBoxWitMessageByWriter(int id);
    }

   
}
