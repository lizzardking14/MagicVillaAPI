using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels
{
    public class NumeroVillaUpdateViewModel
    {
        public NumeroVillaUpdateViewModel()
        {
            NumeroVilla = new NumeroVillaUpdateDto();
        }
        public NumeroVillaUpdateDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
