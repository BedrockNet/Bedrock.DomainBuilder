using System.Linq;
using System.Threading.Tasks;

using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public class BuilderServiceInterface : BuilderBase, IBuilder
    {
        #region Protected Methods
        protected override async Task<int> BuildInternal(BuildSettings settings)
        {
            var filesToCreate = EntityHelper.EntityTypes.Where(e => Roots.Contains(e.Name) && !Enums.Contains(e.Name));
            BuilderFileCountUpdate(filesToCreate.Count());

            await Pause.WaitWhilePausedAsync();

            foreach (var file in filesToCreate)
            {
                var filePath = string.Concat(settings.ServiceInterfacePath, "I", file.Name, "Service", settings.FileExtension);

                await BuildFile<eBuildSectionServiceInterface>(filePath, file, settings);
                await Pause.WaitWhilePausedAsync();

                if (Cancellation.IsCancellationRequested)
                    break;
            }

            return filesToCreate.Count();
        }
        #endregion
    }
}
