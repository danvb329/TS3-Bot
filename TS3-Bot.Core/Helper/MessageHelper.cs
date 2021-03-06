﻿namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using Entity.Messages;
    using Repository;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines the MessageHelper class.
    /// </summary>
    public class MessageHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MessageHelper(DataRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public DataRepository Repository { get; set; }

        /// <summary>
        /// Determines whether this instance [can be message] the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can be message] the specified message; otherwise, <c>false</c>.
        /// </returns>
        public bool CanBeMessage<T>(string message) where T : Message, new()
        {
            var t = new T { Repository = Repository };
            return t.Validate(message.Split(' '));
        }

        /// <summary>
        /// Gets the message information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public T GetMessageInformation<T>(MessageReceivedEventArgs e, string message) where T : Message, new()
        {
            var t = new T { Repository = Repository };
            t.Initialize(e, message.Split(' '));
            return t;
        }

        /// <summary>
        /// Gets the help messages.
        /// </summary>
        /// <param name="groups">The server groups.</param>
        /// <returns>The Help Messages</returns>
        public IEnumerable<string> GetHelpMessages(List<uint> groups)
        {
            var helpMessages = new List<string>(new[] { Repository.Settings.Control.HelpMessage });

            if (Repository.Settings.Control.Help.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Help, groups))
                helpMessages.Add(Repository.Settings.Control.Help.HelpMessage.Trim());

            if (Repository.Settings.Control.Seen.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Seen, groups))
                helpMessages.Add(Repository.Settings.Control.Seen.HelpMessage.Trim());

            if (Repository.Settings.Control.Stick.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Stick, groups))
                helpMessages.Add(Repository.Settings.Control.Stick.HelpMessage.Trim());

            if (Repository.Settings.Control.Moderator.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Moderator, groups))
                helpMessages.Add(Repository.Settings.Control.Moderator.HelpMessage.Trim());

            if (Repository.Settings.Control.SeenModerator.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.SeenModerator, groups))
                helpMessages.Add(Repository.Settings.Control.SeenModerator.HelpMessage.Trim());

            if (Repository.Settings.Control.SeenGroup.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.SeenGroup, groups))
                helpMessages.Add(Repository.Settings.Control.SeenGroup.HelpMessage.Trim());

            if (Repository.Settings.Control.Hours.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Hours, groups))
                helpMessages.Add(Repository.Settings.Control.Hours.HelpMessage.Trim());

            if (Repository.Settings.Control.Punish.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Punish, groups))
                helpMessages.Add(Repository.Settings.Control.Punish.HelpMessage.Trim());

            if (Repository.Settings.Control.Files.Enabled && PermissionHelper.IsGranted(Repository.Settings.Control.Files, groups))
                helpMessages.Add(Repository.Settings.Control.Files.HelpMessage.Trim());

            return helpMessages;
        }
    }
}