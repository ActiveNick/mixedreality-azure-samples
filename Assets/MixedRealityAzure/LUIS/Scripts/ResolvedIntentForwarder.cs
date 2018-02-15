﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Cognitive.LUIS;

namespace Microsoft.MR.LUIS
{
    /// <summary>
    /// An <see cref="IIntentHandler"/> that routes supported intents to resolved scene entities.
    /// </summary>
    public class ResolvedIntentForwarder : IIntentHandler
    {
        #region Member Variables
        private List<string> excludedIntents = new List<string>();
        private List<string> includedIntents = new List<string>();
        #endregion // Member Variables

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool CanHandle(string intentName)
        {
            // Is it explicitly excluded?
            if (excludedIntents.Contains(intentName))
            {
                return false;
            }

            // Is it either wildcard or explicitly included?
            if ((includedIntents.Count == 0) || (includedIntents.Contains(intentName)))
            {
                return true;
            }

            // Not supported
            return false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Handle(Intent intent, LuisMRResult result)
        {
            // Validate
            if (intent == null) throw new ArgumentNullException(nameof(intent));
            if (result == null) throw new ArgumentNullException(nameof(result));

            // Get all resolved entities
            List<EntityMap> resolvedEntities = result.GetAllEntities();

            // Forward to all resolved entities
            foreach (EntityMap map in resolvedEntities)
            {
                // TODO: ExecuteEvents on map.GameObject
            }
        }

        /// <summary>
        /// Gets the list of explicitly excluded intents.
        /// </summary>
        public List<string> ExcludedIntents => excludedIntents;

        /// <summary>
        /// Gets the list of explicitly included intents.
        /// </summary>
        /// <remarks>
        /// If no intents are explicitly included, all intents will be forwarded.
        /// </remarks>
        public List<string> IncludedIntents => includedIntents;
    }
}