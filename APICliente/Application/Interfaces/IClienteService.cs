﻿using Domain.Entities;
using Domain.EntitiesDTO;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClientes();
        Task<Cliente?> GetClienteById(int id);
        Task<Cliente?> GetClienteByCpf(string cpf);
        Task<Cliente> PostCliente(ClienteDTO clienteDTO);
        Task PatchCliente(int idCliente, JsonPatchDocument<Cliente> patchDoc);
        Task PutCliente(int idCliente, Cliente clienteInput);
        Task<Cliente> DeleteCliente(int id);
        Task<Cliente> InativaDeleteCliente(int id);
    }
}
