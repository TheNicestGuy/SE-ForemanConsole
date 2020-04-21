using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        #region Program / Private Members

        private MyIni _theIniParser = new MyIni();
        private MyCommandLine _theCommandParser = new MyCommandLine();
        private readonly IDictionary<string, Action> _knownCommands
            = new Dictionary<string, Action>(StringComparer.InvariantCultureIgnoreCase);

        #endregion Program / Private Members

        #region Program / Constants

        private const string INI_SECTION_NAME = "TNGForemanConsole";
        // WARNING: Do not set the main loop to Update1 (every tick). This causes weird bugs.
        private const UpdateFrequency MAIN_LOOP_FREQUENCY = UpdateFrequency.Update10;
        private const UpdateType MAIN_LOOP_TYPE = UpdateType.Update10;

        #endregion Program / Constants

        public Program()
        {
            this.Runtime.UpdateFrequency = MAIN_LOOP_FREQUENCY;


            // Load state from storage
            if (this._theIniParser.TryParse(this.Storage))
            {
                // ...
            } // if could parse Storage
            this._theIniParser.Clear();

            // TEMP: testing code
            List<IMyTerminalBlock> allBlocks = new List<IMyTerminalBlock>();
            this.GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(
                allBlocks
                , block => MyIni.HasSection(block.CustomData, INI_SECTION_NAME)
            );

            foreach (IMyTerminalBlock thisBlock in allBlocks)
            {
                IMyTextSurfaceProvider thisScreenGroup = thisBlock as IMyTextSurfaceProvider;
                if (null == thisScreenGroup)
                {
                    Echo($"{thisBlock.CustomName} is not an IMyTextSurfaceProvider.");
                }
                else
                {
                    Echo($"{thisBlock.CustomName} provides {thisScreenGroup.SurfaceCount} text surfaces.");
                    for (int i = 0; i < thisScreenGroup.SurfaceCount; i++)
                    {
                        IMyTextSurface thisScreen = thisScreenGroup.GetSurface(i);
                        thisScreen.ContentType = ContentType.TEXT_AND_IMAGE;
                        float thisAspectRatio = thisScreen.SurfaceSize.X / thisScreen.SurfaceSize.Y;
                        try
                        {
                            ScreenProfile thisScreenProfile = ScreenProfile.GetScreenProfile(thisScreenGroup, i);
                            thisScreen.FontSize = thisScreenProfile.BestFontSize;
                        } catch (Exception e)
                        {
                            Echo(e.Message);
                        }
                        //thisScreen.FontSize = thisScreenGroup.GetOptimalFontSize(i, false);
                        thisScreen.WriteText(
                            $"{thisBlock.CustomName}[{i.ToString("f0")}]"
                            + $" ({thisScreenGroup.GetSurface(i).Name})"
                            + $"\n{thisScreen.SurfaceSize.ToString()}"
                            + $" (ratio {thisAspectRatio.ToString()})"
                            + $"\n{thisScreenGroup.GetType().ToString()}"
                            + $" (IMyTextPanel = {(thisScreenGroup is IMyTextPanel).ToString()})"
                            + $"\nDefinition {((IMyTerminalBlock)thisScreenGroup).BlockDefinition.TypeIdString}/{((IMyTerminalBlock)thisScreenGroup).BlockDefinition.SubtypeIdAttribute}\n"
                            + String.Join("\n", new string[] { "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" })
                        );
                    }
                }
            }

        } // Program() constructor

        public void Save()
        {
            this._theIniParser.Clear();
            // ...
            this.Storage = this._theIniParser.ToString();
        }

        public void Main(string argument, UpdateType updateSource)
        {
            // This looks like an automatic call for the main loop.
            if ((updateSource & (MAIN_LOOP_TYPE)) != 0)
            {
                // ...
            }

            // This is NOT an automatic call. Run it through the CLI.
            if ((updateSource & (
                UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100
            )) == 0)
            {
                if (this._theCommandParser.TryParse(argument))
                {
                    Action commandAction;
                    if (this._theCommandParser.ArgumentCount > 0)
                    {
                        string commandName = this._theCommandParser.Argument(0);
                        if (null != commandName)
                        {
                            if (this._knownCommands.TryGetValue(commandName, out commandAction))
                            {
                                commandAction();
                            }
                            else
                            {
                                throw new Exception(
                                    $"Command '{commandName}' does not exist."
                                );
                            }
                        } // check and map command name
                    } // if arguments existed
                } // if could parse command line
                else
                {
                    Echo("Program was run with no argument, from a source other than self-update. Doing nothing.");
                }
            }
        } // Main()
    }
}
