﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Users.Entity.Abstract;
using Users.Entity.Concrete;

namespace Users.Infrastructure
{
    // реализация пользовательской фабрики контроллеров,    // наследуясь от фабрики используемой по умолчанию   
    public class NinjectControllerFactory : DefaultControllerFactory
    {   private IKernel ninjectKernel;
        public NinjectControllerFactory()     {
            // создание контейнера       
            ninjectKernel = new StandardKernel();
            AddBindings();
        } 

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {       // получение объекта контроллера из контейнера        
                // используя его тип       
            return controllerType == null         
                ? null         
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()     {
            // конфигурирование контейнера    
            ninjectKernel.Bind<IContentRepository>().To<EFContentRepository>();
        }   
    } 
}