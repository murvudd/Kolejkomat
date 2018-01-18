using System;

namespace CQRS1
{
    public class Command :EventArgs
    {
        public bool Register = true;
    }
}