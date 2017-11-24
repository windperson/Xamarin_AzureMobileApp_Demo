using System;
using Newtonsoft.Json;

namespace TodoList.Abstractions
{
    public abstract class TableData
    {
        public string Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public byte[] Version { get; set; }
    }
}