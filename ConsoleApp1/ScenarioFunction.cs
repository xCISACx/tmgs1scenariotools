using System;
using System.Collections;

namespace ConsoleApp1
{
	public static class ScenarioFunction
	{
		public enum Group : byte
		{
			System,
			Parameter,
			Date,
			Screen,
			Run,
			Message,
			Character,
			Background,
			Still,
			Graphic,
			Effect,
			SoundEffect,
			Music,
			Environment,
			Voice
		}
		
		public enum Name : ushort
		{
			Call,
			Wait,
			TouchWait,
			TxtCntWait,
			Randam,
			GlobalWork,
			DayCompare,
			InSysParam,
			GetSysParam,
			InPl1Param,
			InPl2Param,
			GetPl1Param,
			GetPl2Param,
			AddPl1Param,
			InBkParam,
			GetBkParam,
			AddBkParam,
			InByParam,
			InCh1Param,
			InCh4yParam,
			GetChParam,
			GetCh1Param,
			GetCh2Param,
			GetCh4yParam,
			AddCh1Param,
			AddCh2Param,
			AddCh4yParam,
			Inevent,
			Getevent,
			AddItem,
			GameClear,
			Max_man,
			Max_man2,
			Max_man3,
			Max_Lady1,
			Wdate_Man,
			Nenga,
			Hatu_Man,
			Hatu_Player,
			Syu_Man,
			Syu_Lady,
			Syu_Player,
			Set_Syu_Partner,
			Makura_Man,
			Makura_Lady,
			dakituki,
			Xmas_Man,
			Xmas_Pre,
			White_Day,
			CanCallFriendly,
			Kitaku_Check,
			Kitaku_Kaiwa,
			Rensyu_Man,
			Rensyu_aft,
			Club_Mas,
			baito_o,
			Master_Sele,
			Test_Check,
			Test_Event,
			bunka_jyu,
			Get_Afterscholl,
			Sinro_Singaku,
			Sinro_Syusyoku,
			SinroJudge,
			Fdance,
			GetIndividualEventAF,
			Nisan_Partner,
			City_Para,
			Max_Command,
			Date_Data,
			InData_Date,
			Date_Check,
			Date_People,
			Date_Spot,
			Date_Nanpa,
			Date_Trable1,
			Date_Trable2,
			Date_Trable3,
			Get_Date_Trable,
			Data_Fation1,
			Data_Fation2,
			Date_Double,
			Hati_Check,
			Get_Date_Place,
			Com_Date_Place,
			SetNextSandayDateEvent,
			AokiDateRsv,
			Event_Check,
			Event_Check2,
			Date_Event,
			SetDateEventSection,
			GetDateEventSection,
			ClearScreen,
			StartShakePlane,
			StopShakePlane,
			WipeIn,
			WipeOut,
			Date_Day,
			Date_Place,
			Kigae,
			Chara_Para,
			Chara_Pro,
			Minigame,
			Minigame_Result,
			GameResult,
			MsgDisp,
			DispMsg,
			CloseMsg,
			MsgSel,
			TextSelect,
			MsgSelRand,
			TextSelectS,
			MessageWindow,
			TextSpeed,
			Sele_Kitaku_Kaiwa,
			Sele_Park2,
			Get_Sele_Park2,
			Sele_Park1,
			Get_Sele_Park1,
			Sele_Man,
			Sele_Man_Result,
			Sele_Syusyoku,
			Sele_Syusyoku_Result,
			Choco_Man,
			Choco_Man_Result,
			Name_Sele_Pre,
			Name_Sele,
			Name_Sele_Result,
			Who,
			WindowPosition,
			Xmas_Sele,
			Xmas_Sele_Result,
			Syu_Sele,
			Syu_Sele_Result,
			Sinro_Sele,
			Sinro_Sele_Result,
			Epilog_Init,
			Epilog_Term,
			Epilog_Text,
			Epilog_Clear,
			Text_Color,
			BlinkStart,
			Blink,
			Chara,
			Bg,
			CarBGOpen,
			CarBGScroll,
			CarBGClose,
			Event,
			Approach,
			Pair_Event,
			EventFace,
			OpenAnim,
			WaitAnim,
			Test_After,
			Nenga_Print,
			OpenParticle,
			CloseParticle,
			PlaySE,
			StopSE,
			PlayStream,
			StopStream,
			PlayBGM,
			StopBGM,
			FadeBGM,
			FadeWait,
			PlayME,
			StopME,
			FadeME,
			PlayVoice,
			Voice_Script,
			Voice_Play
		}

		public static Group GetGroupFromFunction(string name)
		{
			string groupString = name.Split(new[] { '_' }, 2)[0];
			
			if (Enum.TryParse<Group>(groupString, out var group))
			{
				Console.WriteLine($"Parsed group: {group}");
				return group;
			}
			else
			{
				throw new ArgumentException($"Invalid group name: {groupString}");
			}
			
			/*switch(name)
            {

				case Name.Call:
					return Group.System;
				case Name.Voice_Play:
					return Group.Voice;
				case Name.MsgDisp:
					return Group.Message;
				case Name.MsgSelRsltGet:
					return Group.Message;
				case Name.ChPrmSet:
					return Group.Parameter;
				case Name.EnvAutoSet:
					return Group.Environment;
				case Name.EnvPlay:
					return Group.Environment;
				case Name.BGOpen:
					return Group.Background;
				case Name.PlayBGM:
					return Group.Music;
				case Name.PlaySE:
					return Group.SoundEffect;
				case Name.ScrFadeIn:
					return Group.Screen;
				case Name.Wait:
					return Group.System;
				case Name.StopSE:
					return Group.SoundEffect;
				case Name.CloseMsg:
					return Group.Message;
				case Name.ScrFadeOut:
					return Group.Screen;
				case Name.SEWait:
					return Group.SoundEffect;
				case Name.StopBGM:
					return Group.Music;
				case Name.SEVol:
					return Group.SoundEffect;
				case Name.EnvStop:
					return Group.Environment;
				case Name.EfctOpen:
					return Group.Effect;
				case Name.EfctClose:
					return Group.Effect;
				case Name.StlOpen:
					return Group.Still;
				case Name.StlEye:
					return Group.Still;
				case Name.StlMouth:
					return Group.Still;
				case Name.StlClose:
					return Group.Still;
				case Name.ChLayout:
					return Group.Character;
				case Name.ChOpen:
					return Group.Character;
				case Name.ChMotion:
					return Group.Character;
				case Name.ChEye:
					return Group.Character;
				case Name.ChMouth:
					return Group.Character;
				case Name.ChEyeOpenLevel:
					return Group.Character;
				case Name.ChClose:
					return Group.Character;
				case Name.NSSOpen:
					return Group.Contact;
				case Name.BGMVol:
					return Group.Music;
				case Name.NSSEye:
					return Group.Contact;
				case Name.NSSMouth:
					return Group.Contact;
				case Name.NSSCheek:
					return Group.Contact;
				case Name.NSSEyeOpenLevel:
					return Group.Contact;
				case Name.MsgSel:
					return Group.Message;
				case Name.DbgAssert:
					return Group.Debug;
				case Name.NSSClose:
					return Group.Contact;
				case Name.ChPrmTblAdd:
					return Group.Parameter;
				case Name.VoiceEVSPlay:
					return Group.Voice;
				case Name.ChPosition:
					return Group.Character;
				case Name.ChCheek:
					return Group.Character;
				case Name.ScrQuake:
					return Group.Screen;
				case Name.StlEyeOpenLevel:
					return Group.Still;
				case Name.ChNanaType:
					return Group.Character;
				case Name.ChMouthOpenLevel:
					return Group.Character;
				case Name.NSSFace:
					return Group.Contact;
				case Name.MsgDispHide:
					return Group.Message;
				case Name.NSSMouthOpenLevel:
					return Group.Contact;
				case Name.DateInfoGet:
					return Group.Date;
				case Name.MsgExSel:
					return Group.Message;
				case Name.MsgExSelRsltGet:
					return Group.Message;
				case Name.GlWorkSet:
					return Group.System;
				case Name.RunDressUp:
					return Group.Run;
				case Name.GlWorkGet:
					return Group.System;
				case Name.SpEventChk:
					return Group.Event;
				case Name.DateRateSet:
					return Group.Date;
				case Name.ChPrmGet:
					return Group.Parameter;
				case Name.PlPrmSet:
					return Group.Parameter;
				case Name.Rand:
					return Group.System;
				case Name.DateTroubleChk:
					return Group.Date;
				case Name.PlChk:
					return Group.Parameter;
				case Name.BGDateBeforeOpen:
					return Group.Background;
				case Name.MsgDispSksp:
					return Group.Message;
				case Name.ChSet:
					return Group.Character;
				case Name.ChFlgGet:
					return Group.Parameter;
				case Name.ChFlgSet:
					return Group.Parameter;
				case Name.DateOpenChk:
					return Group.Date;
				case Name.DateContentSet:
					return Group.Date;
				case Name.EnvPause:
					return Group.Environment;
				case Name.CGSDAnimDisp:
					return Group.Graphic;
				case Name.CGSDAnimWait:
					return Group.Graphic;
				case Name.CGSDAnimClose:
					return Group.Graphic;
				case Name.DateRateGet:
					return Group.Date;
				case Name.MsgBGSkspRsltGet:
					return Group.Message;
				case Name.PlFlgSet:
					return Group.Parameter;
				case Name.MsgDateResultDisp:
					return Group.Message;
				case Name.MsgDispBGSksp:
					return Group.Message;
				case Name.PlPrmGet:
					return Group.Parameter;
				case Name.ChFace:
					return Group.Character;
				case Name.ChCustomLayout:
					return Group.Character;
				case Name.StlCheek:
					return Group.Still;
				case Name.StlMouthOpenLevel:
					return Group.Still;
				case Name.StlNear:
					return Group.Still;
				case Name.BGDateAfterOpen:
					return Group.Background;
				case Name.MsgHearSel:
					return Group.Message;
				case Name.MsgHearSelRsltGet:
					return Group.Message;
				case Name.RunNearSksp:
					return Group.Run;
				case Name.RunNearSkspRsltGet:
					return Group.Run;
				case Name.RunBGSksp:
					return Group.Run;
				case Name.PlPrmTblAdd:
					return Group.Parameter;
				case Name.CycleCntSet:
					return Group.Event;
				case Name.StlBase:
					return Group.Still;
				case Name.StlBG:
					return Group.Still;
				case Name.StlEffect:
					return Group.Still;
				case Name.HolidayChk:
					return Group.Event;
				case Name.PlFlgGet:
					return Group.Parameter;
				case Name.CGSchoolMeal:
					return Group.Graphic;
				case Name.PlPrmAdd:
					return Group.Parameter;
				case Name.ChChk:
					return Group.Parameter;
				case Name.MsgDsipNcNm:
					return Group.Message;
				case Name.MsgNcNmSelRsltGet:
					return Group.Message;
				case Name.AlbumOpen:
					return Group.System;
				case Name.ADVOpen:
					return Group.System;
				case Name.MsgType:
					return Group.Message;
				case Name.RunEverydaySksp:
					return Group.Run;
				case Name.RunEverydaySkspRsltGet:
					return Group.Run;
				case Name.ChFind:
					return Group.Parameter;
				case Name.RunDatePlan:
					return Group.Run;
				case Name.RunDatePlanRsltGet:
					return Group.Run;
				case Name.DatePlanGet:
					return Group.Date;
				case Name.DateSpotDcd:
					return Group.Date;
				case Name.DatePlanSet:
					return Group.Date;
				case Name.DatePlanAdd:
					return Group.Date;
				case Name.MsgSelRand:
					return Group.Message;
				case Name.DressOpen:
					return Group.System;
				case Name.ChPrmAdd:
					return Group.Parameter;
				case Name.RunMiniGame:
					return Group.Run;
				case Name.RunMiniGameRsltGet:
					return Group.Run;
				case Name.CGTestDisp:
					return Group.Graphic;
				case Name.CycleCntGet:
					return Group.Event;
				case Name.EnvResume:
					return Group.Environment;
				case Name.MsgChSel:
					return Group.Message;
				case Name.MsgChSelRsltGet:
					return Group.Message;
				case Name.CGNewYearCardOpen:
					return Group.Graphic;
				case Name.CGNewYearCardClose:
					return Group.Graphic;
				case Name.OmakeChk:
					return Group.System;
				case Name.EndingOpenPtnGet:
					return Group.System;
				case Name.EndingOpen:
					return Group.System;
				case Name.MsgKokuSel:
					return Group.Message;
				case Name.MsgKokuSelRsltGet:
					return Group.Message;
				case Name.MsgClear:
					return Group.Message;
				case Name.TouchWait:
					return Group.System;
				case Name.RunStaffRoll:
					return Group.Run;
				case Name.VoiceStop:
					return Group.Voice;
				case Name.RunFasType:
					return Group.Run;
				case Name.RunFasFavorite:
					return Group.Run;
				case Name.RunChEmotion:
					return Group.Run;
				case Name.RunChProfile:
					return Group.Run;
				case Name.RunGarden:
					return Group.Run;
				case Name.RunTutorialSksp:
					return Group.Run;


				// The following are unused in the existing game.
				case Name.GlWorkAdd:
					return Group.System;
				case Name.OmakeChGet:
					return Group.System;
				case Name.PlCntSet:
				case Name.PlCntGet:
				case Name.PlCntAdd:
				case Name.ChCntSet:
				case Name.ChCntGet:
				case Name.ChCntAdd:
					return Group.Parameter;

				case Name.SeasonChk:
					return Group.Event;
				case Name.ClothSeasonChk:
					return Group.Event;
				case Name.DatePlanClear:
				case Name.DateContentGet:
					return Group.Date;
				case Name.ScrClear:
				case Name.ScrEfctOpen:
				case Name.ScrEfctClose:
					return Group.Screen;
				case Name.RunShop:
				case Name.RunShopRsltGet:
					return Group.Run;

				case Name.StlFace:
					return Group.Still;
				
				case Name.CGClose:
					return Group.Graphic;
				case Name.EfctParam:
					return Group.Effect;
				case Name.SoundStop:
					return Group.Sound;
				

				case Name.BGMPitch:
				case Name.BGMAisac:
				case Name.BGMPause:
				case Name.BGMResume:
					return Group.Music;
				case Name.EnvVol:
				case Name.EnvPitch:
					return Group.Environment;
				case Name.VoiceWait:
					return Group.Voice;

				default:
					throw new Exception("Unknown");

			}*/
			return Group.System;
			throw new Exception("Unknown");
		}
	}
}
