using System;
using System.Collections.Generic;
using System.Linq;

using Framework.DataTypes.DTO;

using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace Framework.Helpers
{
    public class Infrastructure
    {
        public MenuDTO GetMenuDTO(Aplicacion app)
        {
            var menuDTO = MapApp_MenuDTO(app); // _mapper.Map<MenuDTO>(app);
            var hijos = app.Menu.Where(l1 => l1.PadreId == null).ToList();

            // menuDTO.Childs.AddRange(_mapper.Map<List<MenuDTO>>(hijos));
            menuDTO.Childs.AddRange(MapAppMenu_MenuDTO(hijos));
            hijos = app.Menu.Where(l2 => l2.PadreId != null).OrderBy(h => h.PadreId).ToList();

            hijos.ForEach(h => {
                var padre = menuDTO.Childs.Find(p => p.Id == h.PadreId);
                if (padre.Childs == null) {
                    padre.Childs = new List<MenuDTO>();
                }
                //padre.Childs.Add(_mapper.Map<MenuDTO>(h));
                padre.Childs.Add(MapAppMenu_MenuDTO(h));
            });

            return menuDTO;
        }

        public List<MenuDTO> GetMenusDTO(List<Aplicacion> apps)
        {
            var result = new List<MenuDTO>();

            apps.ForEach(app => {
                result.Add(GetMenuDTO(app));
            });

            return result;

        }

        private MenuDTO MapApp_MenuDTO(Aplicacion app)
        {
            var m = new MenuDTO();

            m.Id = app.Id;
            m.DisplayName = app.Nombre;
            m.DisplayIcon = "";
            m.Url = "";
            m.Description = app.Descripcion;
            m.Childs = new List<MenuDTO>();

            return m;
        }

        private List<MenuDTO> MapAppMenu_MenuDTO(List<AppMenu> menu)
        {
            var m = new List<MenuDTO>();
            foreach (var item in menu)
            {
                m.Add(MapAppMenu_MenuDTO(item));
            }

            return m;
        }

        private MenuDTO MapAppMenu_MenuDTO(AppMenu menu)
        {
            var op = new MenuDTO();

            op.Id = menu.Id;
            op.DisplayName = menu.Nombre;
            op.DisplayIcon = menu.ClaseIcono;
            op.Url = menu.URL;
            op.Description = menu.Descripcion;


            return op;
        }
        
    }
}