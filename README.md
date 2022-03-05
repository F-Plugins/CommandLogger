# CommandLogger
![Discord](https://img.shields.io/discord/742861338233274418?label=Discord&logo=Discord) ![Github All Releases](https://img.shields.io/github/downloads/F-Plugins/CommandLogger/total?label=Downloads) ![GitHub release (latest by date)](https://img.shields.io/github/v/release/F-Plugins/CommandLogger?label=Version)

Logs user commands to chat, discord and more ! Everything configurable !

### Download Now
RocketMod: [ClickMe](https://github.com/F-Plugins/CommandLogger/releases)

## Rocket Mod
### Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<Configuration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <LogAllCommands>true</LogAllCommands>
  <BypassPermission>commandLogger.bypass</BypassPermission>
  <LogToChat>true</LogToChat>
  <ViewChatLogPermission>commandLogger.view</ViewChatLogPermission>
  <DiscordWebhooks>
    <DiscordWebhook url="https://discord.com/api/webhooks/XXXX/XXXX" enabled="false" />
    <DiscordWebhook url="https://discord.com/api/webhooks/XXXX/XXXX" enabled="false" />
  </DiscordWebhooks>
  <CommandsToLog>
    <Command name="ban" />
    <Command name="kick" />
    <Command name="warn" />
    <Command name="item" />
    <Command name="vehicle" />
    <Command name="i" />
    <Command name="v" />
    <Command name="tp" />
    <Command name="tphere" />
  </CommandsToLog>
</Configuration>
```

### Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="ChatNotification" Value="[CommandLogger] {0} has execute the command {1} {2} !" />
</Translations>
```

# Adding your own command logger
1. First reference CommandLogger.dll int your project
2. Create a class that inherits the ICommmandLogger interface. It will inherit a log method that you will use to log commands in your custom command logger
3. Instanciate that class
4. Make sure/wait until the CommandLogger plugin has been loaded
5. Add to the command loggers your own logger. Use `Feli.RocketMod.CommandLogger.Plugin.CommandLoggers.Add(your command logger class);`
