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
    partial class Program
    {
        public class ScreenProfile
        {
            public enum SizeClasses
            {
                OneLine
                , Tiny
                , Small
                , Medium
                , Large
            }

            public SizeClasses SizeClass { get; private set; }

            public float BestFontSize { get; private set; }

            public int LineCount { get; private set; }

            private ScreenProfile() { }

            protected ScreenProfile(SizeClasses sizeClass, float bestFontSize, int lineCount) : this()
            {
                this.SizeClass = sizeClass;
                this.BestFontSize = bestFontSize;
                this.LineCount = lineCount;
            }

            public static ScreenProfile GetScreenProfile(IMyTextSurfaceProvider theBlock, int surfaceIndex)
            {
                return GetStandardScreenProfile(FindScreenType(theBlock, surfaceIndex));
            }

            public enum StandardScreens
            {
                ProgrammableLargeMain // Programmable block console screen
                , ProgrammableLargeKeyboard // Programmable block "keyboard" screen
                , CornerScreenLarge // One-line screen. Several mountings, same dimensions.
                , LCDWideLarge // Double-wide big screen
                , LCDLarge // 1x1 edge-to-edge screen
                , TextPanelLarge // Smaller 1x1 screen
                , ControlStationLarge // Seat with one large screen
                , FlightSeatLarge // Seat with one low-profile screen
                // Seat with desk and five screens
                , ControlSeatLargeMain
                , ControlSeatLargeLeftHand
                , ControlSeatLargeLeftAux
                , ControlSeatLargeRightBox
                , ControlSeatLargeRightTTY
                // Large-ship enclosed cockpit, industrial style with low-angle visibility. (Decorative Pack DLC)
                , IndustrialLargeUpperMain // Far left corner, slightly larger
                , IndustrialLargeUpperLeft
                , IndustrialLargeUpperCenter
                , IndustrialLargeUpperRight
                , IndustrialLargeKeyboard // "Keyboard" screen between operator's hands
                , IndustrialLargeNumpad // "Numpad" screen on the right arm rest. Barely visible.
                // Large-ship enclosed cockpit, standard
                , CockpitLargeUpperLeft
                , CockpitLargeUpperCenter
                , CockpitLargeUpperRight
                , CockpitLargeKeyboard // Lower center, between pilot's hands
                , CockpitLargeLowerLeft
                , CockpitLargeLowerRight
                , ProgrammableSmallMain // Programmable block console screen
                , ProgrammableSmallKeyboard // Programmable block "keyboard" screen
                , LCDWideSmall
                , ControlSeatSmall // Stand-alone seat with one low-profile screen
                // Small-ship enclosed cockpit, standard
                , CockpitSmallCenter
                , CockpitSmallLeft
                , CockpitSmallRight
                , CockpitSmallKeyboard // Between pilot's knees
                // Small-ship enclosed cockpit, fighter style
                , FighterSmallTopCenter // Not rectangular. Has a trapezoidal mask.
                , FighterSmallTopLeft
                , FighterSmallTopRight
                , FighterSmallKeyboard // In front of pilot's legs
                , FighterSmallBottomCenter // Between pilot's knees. Not rectangular; slightly tapered.
                , FighterSmallNumpad // Forward of right stick. Sideways.
                // Small-ship enclosed cockpit, industrial style
                , IndustrialSmallLeft
                , IndustrialSmallCenter
                , IndustrialSmallRight
                , IndustrialSmallKeyboard // Between operator's hands
                , IndustrialSmallNumpad // On right arm rest. Barely visible.
            }

            private static IDictionary<StandardScreens, ScreenProfile> _standardScreenProfiles;
            protected static ScreenProfile GetStandardScreenProfile(StandardScreens screenType)
            {
                if (null == _standardScreenProfiles)
                {
                    _standardScreenProfiles = new Dictionary<StandardScreens, ScreenProfile>
                    {

                        //TODO: Populate all known screen profiles
                        {
                            StandardScreens.ProgrammableLargeMain
                            , new ScreenProfile(SizeClasses.Medium, 0.825f, 13)
                        }
                        ,
                        {
                            StandardScreens.ProgrammableLargeKeyboard
                            , new ScreenProfile(SizeClasses.Small, 1.7f, 8)
                        }
                        ,
                        {
                            StandardScreens.CornerScreenLarge
                            , new ScreenProfile(SizeClasses.OneLine, 9.0f, 1)
                        }
                        ,
                        {
                            StandardScreens.LCDWideLarge
                            , new ScreenProfile(SizeClasses.Large, 1.575f, 11)
                        }
                        ,
                        {
                            StandardScreens.LCDLarge
                            , new ScreenProfile(SizeClasses.Medium, 0.96f, 18)
                        }
                        ,
                        {
                            StandardScreens.TextPanelLarge
                            , new ScreenProfile(SizeClasses.Medium, 1.025f, 10)
                        }
                        ,
                        {
                            StandardScreens.ControlStationLarge
                            , new ScreenProfile(SizeClasses.Large, 0.607f, 17)
                        }
                        ,
                        {
                            StandardScreens.FlightSeatLarge
                            , new ScreenProfile(SizeClasses.Small, 3.0f, 5)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatLargeMain
                            , new ScreenProfile(SizeClasses.Medium, 1.05f, 10)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatLargeLeftHand
                            , new ScreenProfile(SizeClasses.Small, 1.26f, 9)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatLargeRightBox
                            , new ScreenProfile(SizeClasses.Tiny, 1.9f, 6)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatLargeLeftAux
                            , new ScreenProfile(SizeClasses.Tiny, 3.5f, 5)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatLargeRightTTY
                            , new ScreenProfile(SizeClasses.Small, 1.45f, 9)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeUpperMain
                            , new ScreenProfile(SizeClasses.Small, 1.15f, 9)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeUpperLeft
                            , new ScreenProfile(SizeClasses.Medium, 1.2f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeUpperCenter
                            , new ScreenProfile(SizeClasses.Medium, 1.2f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeUpperRight
                            , new ScreenProfile(SizeClasses.Medium, 1.05f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeKeyboard
                            , new ScreenProfile(SizeClasses.Small, 1.3f, 8)
                        }
                        ,
                        {
                            StandardScreens.IndustrialLargeNumpad
                            , new ScreenProfile(SizeClasses.Tiny, 1.0f, 1)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeUpperCenter
                            , new ScreenProfile(SizeClasses.Medium, 1.08f, 11)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeUpperLeft
                            , new ScreenProfile(SizeClasses.Medium, 1.17f, 11)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeUpperRight
                            , new ScreenProfile(SizeClasses.Medium, 1.17f, 11)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeKeyboard
                            , new ScreenProfile(SizeClasses.Small, 1.25f, 8)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeLowerLeft
                            , new ScreenProfile(SizeClasses.Small, 1.65f, 8)
                        }
                        ,
                        {
                            StandardScreens.CockpitLargeLowerRight
                            , new ScreenProfile(SizeClasses.Small, 1.65f, 8)
                        }
                        ,
                        {
                            StandardScreens.ProgrammableSmallMain
                            , new ScreenProfile(SizeClasses.Medium, 1.32f, 13)
                        }
                        ,
                        {
                            StandardScreens.ProgrammableSmallKeyboard
                            , new ScreenProfile(SizeClasses.Small, 1.6f, 8)
                        }
                        ,
                        {
                            StandardScreens.LCDWideSmall
                            , new ScreenProfile(SizeClasses.Large, 1.58f, 11)
                        }
                        ,
                        {
                            StandardScreens.ControlSeatSmall
                            , new ScreenProfile(SizeClasses.Medium, 1.15f, 9)
                        }
                        ,
                        {
                            StandardScreens.CockpitSmallCenter
                            , new ScreenProfile(SizeClasses.Medium, 1.32f, 13)
                        }
                        ,
                        {
                            StandardScreens.CockpitSmallLeft
                            , new ScreenProfile(SizeClasses.Small, 1.45f, 9)
                        }
                        ,
                        {
                            StandardScreens.CockpitSmallRight
                            , new ScreenProfile(SizeClasses.Small, 1.45f, 9)
                        }
                        ,
                        {
                            StandardScreens.CockpitSmallKeyboard
                            , new ScreenProfile(SizeClasses.Small, 1.33f, 9)
                        }
                        ,
                        {
                            // The fighter's main screen is not rectangular. It's more of a
                            // trapezoid, so if the text is not center aligned, some is guaranteed
                            // to be cut off.
                            StandardScreens.FighterSmallTopCenter
                            , new ScreenProfile(SizeClasses.Small, 1.15f, 9)
                        }
                        ,
                        {
                            StandardScreens.FighterSmallTopLeft
                            , new ScreenProfile(SizeClasses.Tiny, 2.25f, 5)
                        }
                        ,
                        {
                            StandardScreens.FighterSmallTopRight
                            , new ScreenProfile(SizeClasses.Tiny, 2.25f, 5)
                        }
                        ,
                        {
                            StandardScreens.FighterSmallKeyboard
                            , new ScreenProfile(SizeClasses.Small, 2.8f, 5)
                        }
                        ,
                        {
                            // This screen is also not rectangular, but slightly tapered at the
                            // bottom. Center alignment is recommended.
                            StandardScreens.FighterSmallBottomCenter
                            , new ScreenProfile(SizeClasses.Tiny, 2.9f, 6)
                        }
                        ,
                        {
                            StandardScreens.FighterSmallNumpad
                            , new ScreenProfile(SizeClasses.Tiny, 2.5f, 7)
                        }
                        ,
                        {
                            StandardScreens.IndustrialSmallLeft
                            , new ScreenProfile(SizeClasses.Medium, 1.25f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialSmallCenter
                            , new ScreenProfile(SizeClasses.Medium, 1.15f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialSmallRight
                            , new ScreenProfile(SizeClasses.Medium, 1.25f, 10)
                        }
                        ,
                        {
                            StandardScreens.IndustrialSmallKeyboard
                            , new ScreenProfile(SizeClasses.Small, 2.15f, 8)
                        }
                        ,
                        {
                            StandardScreens.IndustrialSmallNumpad
                            , new ScreenProfile(SizeClasses.Medium, 1.0f, 17)
                        }
                    };
                }

                if (_standardScreenProfiles.ContainsKey(screenType))
                {
                    return _standardScreenProfiles[screenType];
                }
                else
                {
                    throw new ArgumentException(
                        $"No ScreenProfile found for '{Enum.GetName(typeof(StandardScreens), screenType)}'. This is a bug."
                        , "screenType"
                    );
                }
            }

            protected static StandardScreens FindScreenType(
                IMyTextSurfaceProvider theBlock
                , int surfaceIndex
            )
            {
                if (null == theBlock)
                {
                    throw new ArgumentNullException("theBlock");
                }
                if (surfaceIndex < 0)
                {
                    throw new ArgumentException("surfaceIndex must be non-negative.", "surfaceIndex");
                }
                if (theBlock.SurfaceCount - 1 < surfaceIndex)
                {
                    throw new ArgumentException(
                        $"Requested profile for surfaceIndex {surfaceIndex:n0}, but {((IMyTerminalBlock)theBlock).CustomName} only has {theBlock.SurfaceCount:n0} screen surface(s)."
                        , "surfaceIndex"
                    );
                }

                string exactType = ((IMyTerminalBlock)theBlock).BlockDefinition.TypeIdString + "/" + ((IMyTerminalBlock)theBlock).BlockDefinition.SubtypeIdAttribute;

                switch (exactType)
                {
                    case "MyObjectBuilder_MyProgrammableBlock/LargeProgrammableBlock":
                        if (0 == surfaceIndex)
                            return StandardScreens.ProgrammableLargeMain;
                        if (1 == surfaceIndex)
                            // keyboard screen
                            return StandardScreens.ProgrammableLargeKeyboard;
                        break;
                    case "MyObjectBuilder_TextPanel/LargeBlockCorner_LCD_Flat_1":
                    case "MyObjectBuilder_TextPanel/LargeBlockCorner_LCD_Flat_2":
                    case "MyObjectBuilder_TextPanel/LargeBlockCorner_LCD_1":
                    case "MyObjectBuilder_TextPanel/LargeBlockCorner_LCD_2":
                        // These skinny single-line screens have the same dimensions.
                        return StandardScreens.CornerScreenLarge;
                    case "MyObjectBuilder_TextPanel/LargeLCDPanelWide":
                        return StandardScreens.LCDWideLarge;
                    case "MyObjectBuilder_TextPanel/LargeLCDPanel":
                        return StandardScreens.LCDLarge;
                    case "MyObjectBuilder_TextPanel/LargeTextPanel":
                        return StandardScreens.TextPanelLarge;
                    case "MyObjectBuilder_Cockpit/LargeBlockCockpit":
                        return StandardScreens.ControlStationLarge;
                    case "MyObjectBuilder_Cockpit/CockpitOpen":
                        return StandardScreens.FlightSeatLarge;
                    case "MyObjectBuilder_Cockpit/OpenCockpitLarge":
                        if (0 == surfaceIndex)
                            return StandardScreens.ControlSeatLargeMain;
                        if (1 == surfaceIndex)
                            return StandardScreens.ControlSeatLargeLeftHand;
                        if (2 == surfaceIndex)
                            return StandardScreens.ControlSeatLargeRightBox;
                        if (3 == surfaceIndex)
                            return StandardScreens.ControlSeatLargeLeftAux;
                        if (4 == surfaceIndex)
                            return StandardScreens.ControlSeatLargeRightTTY;
                        break;
                    case "MyObjectBuilder_Cockpit/LargeBlockCockpitIndustrial":
                        if (0 == surfaceIndex)
                            return StandardScreens.IndustrialLargeUpperMain;
                        if (1 == surfaceIndex)
                            return StandardScreens.IndustrialLargeUpperLeft;
                        if (2 == surfaceIndex)
                            return StandardScreens.IndustrialLargeUpperCenter;
                        if (3 == surfaceIndex)
                            return StandardScreens.IndustrialLargeUpperRight;
                        if (4 == surfaceIndex)
                            return StandardScreens.IndustrialLargeKeyboard;
                        if (5 == surfaceIndex)
                            return StandardScreens.IndustrialLargeNumpad;
                        break;
                    case "MyObjectBuilder_Cockpit/LargeBlockCockpitSeat":
                        if (0 == surfaceIndex)
                            return StandardScreens.CockpitLargeUpperCenter;
                        if (1 == surfaceIndex)
                            return StandardScreens.CockpitLargeUpperLeft;
                        if (2 == surfaceIndex)
                            return StandardScreens.CockpitLargeUpperRight;
                        if (3 == surfaceIndex)
                            return StandardScreens.CockpitLargeKeyboard;
                        if (4 == surfaceIndex)
                            return StandardScreens.CockpitLargeLowerLeft;
                        if (5 == surfaceIndex)
                            return StandardScreens.CockpitLargeLowerRight;
                        break;
                    case "MyObjectBuilder_MyProgrammableBlock/SmallProgrammableBlock":
                        if (0 == surfaceIndex)
                            return StandardScreens.ProgrammableSmallMain;
                        if (1 == surfaceIndex)
                            return StandardScreens.ProgrammableSmallKeyboard;
                        break;
                    case "MyObjectBuilder_TextPanel/SmallLCDPanelWide":
                        return StandardScreens.LCDWideSmall;
                    case "MyObjectBuilder_Cockpit/OpenCockpitSmall":
                        return StandardScreens.ControlSeatSmall;
                    case "MyObjectBuilder_Cockpit/SmallBlockCockpit":
                        if (0 == surfaceIndex)
                            return StandardScreens.CockpitSmallCenter;
                        if (1 == surfaceIndex)
                            return StandardScreens.CockpitSmallLeft;
                        if (2 == surfaceIndex)
                            return StandardScreens.CockpitSmallRight;
                        if (3 == surfaceIndex)
                            return StandardScreens.CockpitSmallKeyboard;
                        break;
                    case "MyObjectBuilder_Cockpit/DBSmallBlockFighterCockpit":
                        if (0 == surfaceIndex)
                            return StandardScreens.FighterSmallTopCenter;
                        if (1 == surfaceIndex)
                            return StandardScreens.FighterSmallTopLeft;
                        if (2 == surfaceIndex)
                            return StandardScreens.FighterSmallTopRight;
                        if (3 == surfaceIndex)
                            return StandardScreens.FighterSmallKeyboard;
                        if (4 == surfaceIndex)
                            return StandardScreens.FighterSmallBottomCenter;
                        if (5 == surfaceIndex)
                            return StandardScreens.FighterSmallNumpad;
                        break;
                    case "MyObjectBuilder_Cockpit/SmallBlockCockpitIndustrial":
                        if (0 == surfaceIndex)
                            return StandardScreens.IndustrialSmallLeft;
                        if (1 == surfaceIndex)
                            return StandardScreens.IndustrialSmallCenter;
                        if (2 == surfaceIndex)
                            return StandardScreens.IndustrialSmallRight;
                        if (3 == surfaceIndex)
                            return StandardScreens.IndustrialSmallKeyboard;
                        if (4 == surfaceIndex)
                            return StandardScreens.IndustrialSmallNumpad;
                        break;
                }
                throw new Exception(
                    $"Screen type not known for block '{((IMyTerminalBlock)theBlock).CustomName}'. Is it non-vanilla?"
                    + $"\nType Definition: '{exactType}'"
                    + $"\nsurfaceIndex: {surfaceIndex:n0}"
                );
            }
        }
    }
}
