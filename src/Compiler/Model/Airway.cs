﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Airway : AbstractSectorElement, ICompilable
    {
        public Airway(
            string identifier,
            AirwayType type,
            Point startPoint,
            Point endPoint,
            string comment
        ) 
            : base(comment)
        {
            Identifier = identifier;
            Type = type;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public string Identifier { get; }
        public AirwayType Type { get; }
        public Point StartPoint { get; }
        public Point EndPoint { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1} {2}{3}\r\n",
                this.Identifier,
                this.StartPoint.Compile(),
                this.EndPoint.Compile(),
                this.CompileComment()
            );
        }
    }
}