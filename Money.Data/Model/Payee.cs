﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Model
{
    public class Payee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PayeeAlternateName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PayeeId { get; set; }
    }
}
