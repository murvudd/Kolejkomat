﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API1.Models
{
    public class PositionInQueue : Person
    {
        public int Pos { get; set; } 
        public DateTime Date { get; set; }
        public string Issue { get; set; } = "";

        public PositionInQueue(Person a, int x, DateTime date, string str)
            : base(a)
        {
            //Pos = new Position(str);
            Pos = x;
            Date = date;
            Issue = str;
            
        }

        public PositionInQueue(Person a, int x, DateTime date)
            : base(a)
        {
            //Pos = new Position(str);
            Pos = x;
            Date = date;

        }

    }

    
    public class Position
    {
        private int _a { get; set; }
        private int A
        {
            get { return _a; }
            set
            {
                _a =_a+ 1;
                if (_a == 1000) { _a = 1; }
            }
        }
        private int _b { get; set; }
        private int B
        {
            get { return _b; }
            set
            {
                if (_b == 1000) { _b = 1; }
                _b += 1;
            }
        }
        private int _c { get; set; }
        private int C
        {
            get { return _c; }
            set
            {
                if (_c == 1000) { _c = 1; }
                _c += 1;
            }
        }
        
        public string Case {
            get { return Case; }
            set
            {
                if (value == nameof(A)) { A = 10; Case = value; }
                if (value == nameof(B)) { B = 10; Case = value; }
                if (value == nameof(C)) { C = 10; Case = value; }
            }
        }
        
        public Position(string str)
        {
            Case = str;
        }    
       
    }
}