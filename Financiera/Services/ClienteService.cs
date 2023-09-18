using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;
using System.Collections.Generic;

namespace Financiera.Services
{
    public class ClienteService
    {
        private readonly IRepository<Cliente> _context;
        private readonly IRepository<Genero> _generoContext;

        private readonly ClienteDomain _clienteDomain;
        private readonly IMapper _mapper;

        public ClienteService(IRepository<Cliente> context, IRepository<Genero> generoContext, IMapper mapper, ClienteDomain clienteDomain)
        {
            _context = context;
            _generoContext = generoContext;
            _mapper = mapper;
            _clienteDomain = clienteDomain;
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            var clientes = _context.Find(x => x.Estado == true, X => X.Genero);
            var clienteDTOs = ClienteDTO.FromModelToDTO(clientes);
            return clienteDTOs;
        }

        public async Task<Response> GetById(int id)
        {
            var cliente = await _context.GetById(id, x => x.Genero);
            if (cliente == null)
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.This} {Constantes.Client} con id: {id} {Constantes.DoesNotExist}"
                };
            }
            var clienteDTO = ClienteDTO.FromModelToDTO(cliente);
            return new Response
            {
                Status = Constantes.Sucess,
                Message = "Recuperado Correctamente",
                Data = clienteDTO
            };
        }

        public async Task<Response> SaveCliente(ClienteDTO clienteDTO)
        {
            var clientes = _context.GetAll();
            var existingCliente = clientes.FirstOrDefault(x=>x.Identificacion.Equals(clienteDTO.Identificacion));
            if (_clienteDomain.DuplicateCliente(existingCliente,clienteDTO))
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.This} {Constantes.Client} con identificaion:{clienteDTO.Identificacion} {Constantes.Duplicated}"                
                };
            }


            var cliente = _mapper.Map<Cliente>(clienteDTO);
            var gender = _generoContext.Find(x => x.Id == clienteDTO.GeneroId).FirstOrDefault();
            cliente.Genero = gender;
            _context.Add(cliente);
            _context.SaveChanges();
            return new Response
            {
                Status = Constantes.Sucess,
                Message = $"{Constantes.This} {Constantes.Client} {Constantes.SucefullyRegister}",
                Data = clienteDTO
            };
                       
        }
        public async Task<Response> EditClient(ClienteDTO clientDTO)
        {
            var currentClientDTO = await GetById(clientDTO.Id);
            if (currentClientDTO.Data != null)
            {
                var currentClient = _mapper.Map<Cliente>(clientDTO);
                _context.Update(currentClient);
                _context.SaveChanges();
                return new Response
                {
                    Status = Constantes.Sucess,
                    Message = $"Cliente {clientDTO.Nombre} modificado exitosamente!"
                };
            }
            return new Response
            {
                Status = Constantes.Failed,
                Message = $"Cliente {clientDTO.Identificacion} {Constantes.DoesNotExist}!"
            };
        }
        
        public async Task<Response> DeleteClient(int id)
        {
            var currentClientDTO = await GetById(id);
            if (currentClientDTO.Data != null)
            {
                var currentClient = _mapper.Map<Cliente>(currentClientDTO.Data);

                currentClient.Estado = false;
                _context.Update(currentClient);
                _context.SaveChanges();
                return new Response
                {
                    Status = Constantes.Sucess,
                    Message = $"Cliente {currentClient.Identificacion} borrado correctamente!"
                };
            }
            return new Response
            {
                Status = Constantes.Failed,
                Message = $"Cliente {Constantes.DoesNotExist}!"
            };
        }
    }
}
