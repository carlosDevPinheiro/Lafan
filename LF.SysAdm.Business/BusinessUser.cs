using LF.SysAdm.Business.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.User;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Querys.User;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Shared.Utils;
using System;
using System.Linq;

namespace LF.SysAdm.Business
{
    public class BusinessUser : BaseBusiness, IBusinessUser
    {
        IRepositoryUser _repository;
        public BusinessUser(IRepositoryUser repos, IUnityOfWork uow)
            : base(uow)
        {
            _repository = repos;
        }

        public ObjectRequest GetUser(Guid Id)
        {
            var user = _repository.GetEntity(Id);

            if (user == null) return new ObjectRequest().CreateObjectRequest("Usuario não cadastrado no Sistema", false);

            return new ObjectRequest().CreateObjectRequest(_repository.GetUser(Id), true);
        }

        public Users GetUserOAuth(string email, string password) => _repository.Authenticate(email.ToLower(), SecurityPassword.Encrypt(password.Trim()));


        public ObjectRequest GetUsers()
        {
            var users = _repository.GetAllEntity();
            return new ObjectRequest().CreateObjectRequest(users.Select(x => new UserQuery
            {
                Name = x.Name,
                Active = x.Active,
                DateofChange = x.DateofChange,
                Email = x.Email,
                Profile = x.GetProfile(x.Profile),
                RegistrationDate = x.RegistrationDate,
                ID = x.ID

            }).ToList(), true);
        }

        public ObjectRequest NewUser(RegisterUsersCommand cmd)
        {
            if (_repository.IsUserExist(cmd.Email)) return new ObjectRequest()
                    .CreateObjectRequest("Usuario ja esta Registrado no Sistema !", false);

            Users user = new Users(cmd.Name, cmd.Email, cmd.Password);
            _repository.AddEntity(user);

            if (Commit(user))
                return new ObjectRequest()
                    .CreateObjectRequest($"Registro de {user.Name} Criado com Sucesso {user.ID} {user.Email}", true);

            return new ObjectRequest().CreateErrorNotification(user.ListErrors());
        }

        public ObjectRequest Remove(Guid id)
        {
            var user = _repository.GetEntity(id);
            _repository.Delete(user);
            if (Commit(user))
                return new ObjectRequest().CreateObjectRequest($"Usuario {user.Email} Removido com Sucesso", true);
            return new ObjectRequest().CreateObjectRequest("Não foi possivel remover usuario", false);
        }

        public ObjectRequest UpdateUser(EditUserCommand cmd)
        {
            Users user = _repository.GetEntity(cmd.ID);
            if (user == null)
                return new ObjectRequest().CreateObjectRequest($"Usuario  não Cadastrado no Sistema ou senhas nao identicas", false);

            user.Update(cmd.Name, cmd.Email, cmd.Password);
            _repository.EditEntity(user);

            if (Commit(user))
                return new ObjectRequest()
                    .CreateObjectRequest($"Registro de {user.Name} Alterado com Sucesso {user.ID} -  {user.Email}", true);
            else
                return new ObjectRequest().CreateErrorNotification(user.ListErrors());
        }
    }
}
