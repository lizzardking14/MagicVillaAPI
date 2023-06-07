using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels
{
    public class NumeroVillaDeleteViewModel
    {
        public NumeroVillaDeleteViewModel()
        {
            NumeroVilla = new NumeroVillaDto();
        }
        public NumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
