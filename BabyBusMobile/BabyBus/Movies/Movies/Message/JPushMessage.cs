﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace BabyBus.Message
{
    public class JPushMessage : MvxMessage
    {
        public JPushMessage(object sender) : base(sender) {
        }
    }
}
