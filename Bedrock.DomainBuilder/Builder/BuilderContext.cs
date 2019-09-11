using System.Threading.Tasks;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public class BuilderContext : BuilderBase, IBuilder
    {
        #region Protected Methods
        protected override async Task<int> BuildInternal(BuildSettings settings)
        {
            BuilderFileCountUpdate(1);

            await Pause.WaitWhilePausedAsync();

            var filePath = string.Concat(settings.ContextPath, settings.Domain, SchemaCamelCased, "Context", settings.FileExtension);

            await BuildFile<eBuildSectionContext>(filePath, null, settings);

            return 1;
        }
        #endregion
    }
}
