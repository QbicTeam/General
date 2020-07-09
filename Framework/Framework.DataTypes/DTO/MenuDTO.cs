using System.Collections.Generic;

namespace Framework.DataTypes.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayIcon { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<MenuDTO> Childs { get; set; }

    }
}