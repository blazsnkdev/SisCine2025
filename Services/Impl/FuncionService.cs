using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Interfaces;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class FuncionService : IFuncionService
    {
        private readonly IFuncionRepository _funcionRepository;

        //Inyectamos el repositorio
        public FuncionService(IFuncionRepository funcionRepository)
        {
            _funcionRepository = funcionRepository;
        }


        public void Agregar(Funcion funcion)
        {
            TimeOnly horaInicio = funcion.HoraInicio;
            TimeOnly horaFin = funcion.HoraFin;
            if(horaFin <= horaInicio)
            {
                throw new ArgumentException("La hora de fin debe ser posterior a la hora de inicio.");
            }
            _funcionRepository.Agregar(funcion);
        }
    }
}
