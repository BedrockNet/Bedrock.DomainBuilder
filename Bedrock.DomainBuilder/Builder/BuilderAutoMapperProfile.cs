using System;
using System.Linq;
using System.Threading.Tasks;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public class BuilderAutoMapperProfile : BuilderBase, IBuilder
    {
        #region Protected Methods
        protected override async Task<int> BuildInternal(BuildSettings settings)
        {
            var filesToCreate = EntityHelper.EntityTypes.Where(i => !settings.EntityExclusions.Any(e => i.Name.StartsWith(e, StringComparison.CurrentCultureIgnoreCase)));
            BuilderFileCountUpdate(filesToCreate.Count());

            await Pause.WaitWhilePausedAsync();

            foreach (var file in filesToCreate)
            {
                var filePath = string.Concat(settings.AutoMapperProfilePath, file.Name + "Profile", settings.FileExtension);

                await BuildFile<eBuildSectionAutoMapperProfile>(filePath, file, settings);
                await Pause.WaitWhilePausedAsync();

                if (Cancellation.IsCancellationRequested)
                    break;
            }

            return filesToCreate.Count();
        }
        #endregion
    }
}
