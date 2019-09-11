using System;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public static class AFBuilderFactory
    {
        #region Methods
        public static IBuilder Create(eBuilder builder, bool isActive, PauseToken pause)
        {
            IBuilder returnValue = null;

            switch (builder)
            {
                case eBuilder.Entity:
                    {
                        returnValue = new BuilderEntity { Pause = pause };
                        break;
                    }
                case eBuilder.EntityPartial:
                    {
                        returnValue = new BuilderEntityPartial { Pause = pause };
                        break;
                    }
                case eBuilder.Mapping:
                    {
                        returnValue = new BuilderMapping { Pause = pause };
                        break;
                    }
                case eBuilder.Context:
                    {
                        returnValue = new BuilderContext { Pause = pause };
                        break;
                    }
                case eBuilder.Service:
                    {
                        returnValue = new BuilderService { Pause = pause };
                        break;
                    }
                case eBuilder.ServiceInterface:
                    {
                        returnValue = new BuilderServiceInterface { Pause = pause };
                        break;
                    }
                case eBuilder.Enumeration:
                    {
                        returnValue = new BuilderEnumeration { Pause = pause };
                        break;
                    }
                case eBuilder.AppContext:
                    {
                        returnValue = new BuilderAppContext { Pause = pause };
                        break;
                    }
                case eBuilder.Contract:
                    {
                        returnValue = new BuilderContract { Pause = pause };
                        break;
                    }
                case eBuilder.AppService:
                    {
                        returnValue = new BuilderAppService { Pause = pause };
                        break;
                    }
                case eBuilder.AppServiceInterface:
                    {
                        returnValue = new BuilderAppServiceInterface { Pause = pause };
                        break;
                    }
                case eBuilder.ControllerApi:
                    {
                        returnValue = new BuilderControllerApi { Pause = pause };
                        break;
                    }
                case eBuilder.ControllerMvc:
                    {
                        returnValue = new BuilderControllerMvc { Pause = pause };
                        break;
                    }
                case eBuilder.AutoMapperProfile:
                    {
                        returnValue = new BuilderAutoMapperProfile { Pause = pause };
                        break;
                    }
                default:
                    throw new NotSupportedException(string.Concat("Unsupported builder  :", builder.ToString()));
            }

            returnValue.BuilderType = builder;
            returnValue.IsActive = isActive;

            return returnValue;
        }
        #endregion
    }
}
