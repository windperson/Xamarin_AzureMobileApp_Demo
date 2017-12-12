using System;
using System.Collections.Generic;
using System.Text;

namespace SharedInterface
{
    public interface ITodoItem
    {
        string Text { get; set; }

        bool Complete { get; set; }
    }
}
