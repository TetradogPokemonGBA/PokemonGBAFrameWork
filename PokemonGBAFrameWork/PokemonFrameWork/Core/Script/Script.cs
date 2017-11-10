/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:08
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
using PokemonGBAFrameWork.ComandosScript;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Script.
	/// </summary>
	public class Script:IDeclaracion,IBloqueConNombre
	{
		public static readonly Creditos Creditos;
		const string ENTER = "\r\n";
		//enter con el formato correcto// \r\n
		public const byte RETURN = 0x3;
		public const byte END = 0x2;
		
		public static Hex OffsetInicioDynamic = "800000";
		Llista<Comando> comandosScript;
		string nombreBloque;
		static Script()
		{
			Creditos = new Creditos();
			Creditos.Add("XSE", "HackMew", "Hacer la aplicacion y sus explicaciones");
		}
		
		public Script()
		{
			comandosScript = new Llista<Comando>();
		}
		public Script(RomGba rom, int offsetScript)
			: this(rom.Data.Bytes, offsetScript)
		{
			
		}
		public Script(byte[] bytesScript, int offset = 0)
			: this()
		{
			unsafe {
				fixed(byte* ptBytesScirpt=bytesScript)
					Cargar(ptBytesScirpt, offset);
				
			}
			
		}
		public unsafe Script(byte* ptRom, int offsetScript)
			: this()
		{
			Cargar(ptRom, offsetScript);
		}
		unsafe void Cargar(byte* ptrRom, int offsetScript)
		{
			//quizas no siempre acaba en end o return y acaba por ejemplo llamando a otra funcion...por mirar...
			//obtengo los comandos hasta encontrar return o end
			//podria ser que un byteComandoActual depende de una edicion u otra llama a una funcion...u otra???sino no me lo explico...
			//RojoFuegoEsp->1657F0 el call que hay llama a un script que contiene comandos de Esmeralda...es raro...
			byte byteComandoActual;
			Comando comandoActual;
			
			do {
				comandoActual = null;
				byteComandoActual = ptrRom[offsetScript];
				offsetScript++;
				switch (byteComandoActual) {
				//pongo los comandos
					case Nop.ID:
						comandoActual = new Nop(ptrRom, offsetScript);
						break;
					case Nop1.ID:
						comandoActual = new Nop1(ptrRom, offsetScript);
						break;
					case Call.ID:
						comandoActual = new Call(ptrRom, offsetScript);
						break;
					case Goto.ID:
						comandoActual = new Goto(ptrRom, offsetScript);
						break;
					case If1.ID:
						comandoActual = new If1(ptrRom, offsetScript);
						break;
					case If2.ID:
						comandoActual = new If2(ptrRom, offsetScript);
						break;
					case Gotostd.ID:
						comandoActual = new Gotostd(ptrRom, offsetScript);
						break;
					case Callstd.ID:
						comandoActual = new Callstd(ptrRom, offsetScript);
						break;
					case Gotostdif.ID:
						comandoActual = new Gotostdif(ptrRom, offsetScript);
						break;
					case Callstdif.ID:
						comandoActual = new Callstdif(ptrRom, offsetScript);
						break;
					case Jumpram.ID:
						comandoActual = new Jumpram(ptrRom, offsetScript);
						break;
					case Killscript.ID:
						comandoActual = new Killscript(ptrRom, offsetScript);
						break;
					case SetByte.ID:
						comandoActual = new SetByte(ptrRom, offsetScript);
						break;
					case LoadPointer.ID:
						comandoActual = new LoadPointer(ptrRom, offsetScript);
						break;
					case SetByte2.ID:
						comandoActual = new SetByte2(ptrRom, offsetScript);
						break;
					case WriteByteToOffset.ID:
						comandoActual = new WriteByteToOffset(ptrRom, offsetScript);
						break;
					case LoadByteFromPointer.ID:
						comandoActual = new LoadByteFromPointer(ptrRom, offsetScript);
						break;
					case SetFarByte.ID:
						comandoActual = new SetFarByte(ptrRom, offsetScript);
						break;
					case Copyscriptbanks.ID:
						comandoActual = new Copyscriptbanks(ptrRom, offsetScript);
						break;
					case CopyByte.ID:
						comandoActual = new CopyByte(ptrRom, offsetScript);
						break;
					case SetVar.ID:
						comandoActual = new SetVar(ptrRom, offsetScript);
						break;
					case AddVar.ID:
						comandoActual = new AddVar(ptrRom, offsetScript);
						break;
					case SubVar.ID:
						comandoActual = new SubVar(ptrRom, offsetScript);
						break;
					case CopyVar.ID:
						comandoActual = new CopyVar(ptrRom, offsetScript);
						break;
					case CopyVarIfNotZero.ID:
						comandoActual = new CopyVarIfNotZero(ptrRom, offsetScript);
						break;
					case CompareBanks.ID:
						comandoActual = new CompareBanks(ptrRom, offsetScript);
						break;
					case CompareBankToByte.ID:
						comandoActual = new CompareBankToByte(ptrRom, offsetScript);
						break;
					case CompareBankToFarByte.ID:
						comandoActual = new CompareBankToFarByte(ptrRom, offsetScript);
						break;
					case CompareFarByteToBank.ID:
						comandoActual = new CompareBankToFarByte(ptrRom, offsetScript);
						break;
					case CompareFarByteToByte.ID:
						comandoActual = new CompareFarByteToByte(ptrRom, offsetScript);
						break;
					case CompareFarBytes.ID:
						comandoActual = new CompareFarBytes(ptrRom, offsetScript);
						break;
					case Compare.ID:
						comandoActual = new Compare(ptrRom, offsetScript);
						break;
					case CompareVars.ID:
						comandoActual = new CompareVars(ptrRom, offsetScript);
						break;
					case CallAsm.ID:
						comandoActual = new CallAsm(ptrRom, offsetScript);
						break;
					case Cmd24.ID:
						comandoActual = new Cmd24(ptrRom, offsetScript);
						break;
					case Special.ID:
						comandoActual = new Special(ptrRom, offsetScript);
						break;
					case Special2.ID:
						comandoActual = new Special2(ptrRom, offsetScript);
						break;
					case WaitState.ID:
						comandoActual = new WaitState(ptrRom, offsetScript);
						break;
					case Pause.ID:
						comandoActual = new Pause(ptrRom, offsetScript);
						break;
					case SetFlag.ID:
						comandoActual = new SetFlag(ptrRom, offsetScript);
						break;
					case ClearFlag.ID:
						comandoActual = new ClearFlag(ptrRom, offsetScript);
						break;
					case CheckFlag.ID:
						comandoActual = new CheckFlag(ptrRom, offsetScript);
						break;
					case Cmd2C.ID:
						comandoActual = new Cmd2C(ptrRom, offsetScript);
						break;
					case CheckSound.ID:
						comandoActual = new CheckSound(ptrRom, offsetScript);
						break;
					case FanFare.ID:
						comandoActual = new FanFare(ptrRom, offsetScript);
						break;
					case WaitFanFare.ID:
						comandoActual = new WaitFanFare(ptrRom, offsetScript);
						break;
					case PlaySong.ID:
						comandoActual = new PlaySong(ptrRom, offsetScript);
						break;
					case PlaySong2.ID:
						comandoActual = new PlaySong2(ptrRom, offsetScript);
						break;
					case FadeDefault.ID:
						comandoActual = new FadeDefault(ptrRom, offsetScript);
						break;
					case FadeSong.ID:
						comandoActual = new FadeSong(ptrRom, offsetScript);
						break;
					case FadeOut.ID:
						comandoActual = new FadeOut(ptrRom, offsetScript);
						break;
					case FadeIn.ID:
						comandoActual = new FadeIn(ptrRom, offsetScript);
						break;
					case CheckDailyFlags.ID:
						comandoActual = new CheckDailyFlags(ptrRom, offsetScript);
						break;
					case ResetVars.ID:
						comandoActual = new ResetVars(ptrRom, offsetScript);
						break;
					case Sound.ID:
						comandoActual = new Sound(ptrRom, offsetScript);
						break;
					case Warp.ID:
						comandoActual = new Warp(ptrRom, offsetScript);
						break;
					case WarpMuted.ID:
						comandoActual = new WarpMuted(ptrRom, offsetScript);
						break;
					case WarpWalk.ID:
						comandoActual = new WarpWalk(ptrRom, offsetScript);
						break;
					case WarpHole.ID:
						comandoActual = new WarpHole(ptrRom, offsetScript);
						break;
					case WarpTeleport.ID:
						comandoActual = new WarpTeleport(ptrRom, offsetScript);
						break;
					case Warp3.ID:
						comandoActual = new Warp3(ptrRom, offsetScript);
						break;
					case SetWarpplace.ID:
						comandoActual = new SetWarpplace(ptrRom, offsetScript);
						break;
					case Warp4.ID:
						comandoActual = new Warp4(ptrRom, offsetScript);
						break;
					case Warp5.ID:
						comandoActual = new Warp5(ptrRom, offsetScript);
						break;
					case GetPlayerPos.ID:
						comandoActual = new GetPlayerPos(ptrRom, offsetScript);
						break;
					case CountPokemon.ID:
						comandoActual = new CountPokemon(ptrRom, offsetScript);
						break;
					case AddItem.ID:
						comandoActual = new AddItem(ptrRom, offsetScript);
						break;
					case RemoveItem.ID:
						comandoActual = new RemoveItem(ptrRom, offsetScript);
						break;
					case CheckItemRoom.ID:
						comandoActual = new CheckItemRoom(ptrRom, offsetScript);
						break;
					case CheckItem.ID:
						comandoActual = new CheckItem(ptrRom, offsetScript);
						break;
					case CheckItemType.ID:
						comandoActual = new CheckItemType(ptrRom, offsetScript);
						break;
					case AddPcItem.ID:
						comandoActual = new AddPcItem(ptrRom, offsetScript);
						break;
					case CheckPcItem.ID:
						comandoActual = new CheckPcItem(ptrRom, offsetScript);
						break;
					case AddDecoration.ID:
						comandoActual = new AddDecoration(ptrRom, offsetScript);
						break;
					case RemoveDecoration.ID:
						comandoActual = new RemoveDecoration(ptrRom, offsetScript);
						break;
					case TestDecoration.ID:
						comandoActual = new TestDecoration(ptrRom, offsetScript);
						break;
					case CheckDecoration.ID:
						comandoActual = new CheckDecoration(ptrRom, offsetScript);
						break;
					case ApplyMovement.ID:
						comandoActual = new ApplyMovement(ptrRom, offsetScript);
						break;
					case ApplyMovementPos.ID:
						comandoActual = new ApplyMovementPos(ptrRom, offsetScript);
						break;
					case WaitMovement.ID:
						comandoActual = new WaitMovement(ptrRom, offsetScript);
						break;
					case WaitMovementPos.ID:
						comandoActual = new WaitMovementPos(ptrRom, offsetScript);
						break;
					case HideSprite.ID:
						comandoActual = new HideSprite(ptrRom, offsetScript);
						break;
					case HideSpritePos.ID:
						comandoActual = new HideSpritePos(ptrRom, offsetScript);
						break;
					case ShowSprite.ID:
						comandoActual = new ShowSprite(ptrRom, offsetScript);
						break;
					case ShowSpritePos.ID:
						comandoActual = new ShowSpritePos(ptrRom, offsetScript);
						break;
					case MoveSprite.ID:
						comandoActual = new MoveSprite(ptrRom, offsetScript);
						break;
					case SpriteVisible.ID:
						comandoActual = new SpriteVisible(ptrRom, offsetScript);
						break;
					case SpriteInvisible.ID:
						comandoActual = new SpriteInvisible(ptrRom, offsetScript);
						break;
					case Faceplayer.ID:
						comandoActual = new Faceplayer(ptrRom, offsetScript);
						break;
					case SpriteFace.ID:
						comandoActual = new SpriteFace(ptrRom, offsetScript);
						break;
					case Trainerbattle.ID:
						comandoActual = new Trainerbattle(ptrRom, offsetScript);
						break;
					case RepeatTrainerBattle.ID:
						comandoActual = new RepeatTrainerBattle(ptrRom, offsetScript);
						break;
					case EndTrainerBattle.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case EndTrainerBattle2.ID:
						comandoActual = new EndTrainerBattle2(ptrRom, offsetScript);
						break;
					case CheckTrainerFlag.ID:
						comandoActual = new CheckTrainerFlag(ptrRom, offsetScript);
						break;
					case ClearTrainerFlag.ID:
						comandoActual = new ClearTrainerFlag(ptrRom, offsetScript);
						break;
					case SetTrainerFlag.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case MoveSprite2.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case MoveOffScreen.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case SpriteBehave.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case WaitMsg.ID:
						comandoActual = new EndTrainerBattle(ptrRom, offsetScript);
						break;
					case PrepareMsg.ID:
						comandoActual = new PrepareMsg(ptrRom, offsetScript);
						break;
					case CloseOnKeyPress.ID:
						comandoActual = new CloseOnKeyPress(ptrRom, offsetScript);
						break;
					case LockAll.ID:
						comandoActual = new LockAll(ptrRom, offsetScript);
						break;
					case Lock.ID:
						comandoActual = new Lock(ptrRom, offsetScript);
						break;
					case ReleaseAll.ID:
						comandoActual = new ReleaseAll(ptrRom, offsetScript);
						break;
					case Release.ID:
						comandoActual = new Release(ptrRom, offsetScript);
						break;
					case WaitKeyPress.ID:
						comandoActual = new WaitKeyPress(ptrRom, offsetScript);
						break;
					case YesNoBox.ID:
						comandoActual = new YesNoBox(ptrRom, offsetScript);
						break;
					case Multichoice.ID:
						comandoActual = new Multichoice(ptrRom, offsetScript);
						break;
					case Multichoice2.ID:
						comandoActual = new Multichoice2(ptrRom, offsetScript);
						break;
					case Multichoice3.ID:
						comandoActual = new Multichoice3(ptrRom, offsetScript);
						break;
					case ShowBox.ID:
						comandoActual = new ShowBox(ptrRom, offsetScript);
						break;
					case HideBox.ID:
						comandoActual = new HideBox(ptrRom, offsetScript);
						break;
					case ClearBox.ID:
						comandoActual = new ClearBox(ptrRom, offsetScript);
						break;
					case ShowPokePic.ID:
						comandoActual = new ShowPokePic(ptrRom, offsetScript);
						break;
					case HidePokePic.ID:
						comandoActual = new HidePokePic(ptrRom, offsetScript);
						break;
					case ShowContestWinner.ID:
						comandoActual = new ShowContestWinner(ptrRom, offsetScript);
						break;
					case Braille.ID:
						comandoActual = new Braille(ptrRom, offsetScript);
						break;
					case GivePokemon.ID:
						comandoActual = new GivePokemon(ptrRom, offsetScript);
						break;
					case GiveEgg.ID:
						comandoActual = new GiveEgg(ptrRom, offsetScript);
						break;
					case SetPkmnPP.ID:
						comandoActual = new SetPkmnPP(ptrRom, offsetScript);
						break;
					case CheckAttack.ID:
						comandoActual = new CheckAttack(ptrRom, offsetScript);
						break;
					case BufferPokemon.ID:
						comandoActual = new BufferPokemon(ptrRom, offsetScript);
						break;
					case BufferFirstPokemon.ID:
						comandoActual = new BufferFirstPokemon(ptrRom, offsetScript);
						break;
					case BufferPartyPokemon.ID:
						comandoActual = new BufferPartyPokemon(ptrRom, offsetScript);
						break;
					case BufferItem.ID:
						comandoActual = new BufferItem(ptrRom, offsetScript);
						break;
					case BufferDecoration.ID:
						comandoActual = new BufferDecoration(ptrRom, offsetScript);
						break;
					case BufferAttack.ID:
						comandoActual = new BufferAttack(ptrRom, offsetScript);
						break;
					case BufferNumber.ID:
						comandoActual = new BufferNumber(ptrRom, offsetScript);
						break;
					case BufferStd.ID:
						comandoActual = new BufferStd(ptrRom, offsetScript);
						break;
					case BufferString.ID:
						comandoActual = new BufferString(ptrRom, offsetScript);
						break;
					case PokeMart.ID:
						comandoActual = new PokeMart(ptrRom, offsetScript);
						break;
					case PokeMart2.ID:
						comandoActual = new PokeMart2(ptrRom, offsetScript);
						break;
					case PokeMart3.ID:
						comandoActual = new PokeMart3(ptrRom, offsetScript);
						break;
					case PokeCasino.ID:
						comandoActual = new PokeCasino(ptrRom, offsetScript);
						break;
					case Cmd8A.ID:
						comandoActual = new Cmd8A(ptrRom, offsetScript);
						break;
					case ChooseContestPkmn.ID:
						comandoActual = new ChooseContestPkmn(ptrRom, offsetScript);
						break;
					case StartContest.ID:
						comandoActual = new StartContest(ptrRom, offsetScript);
						break;
					case ShowContestResults.ID:
						comandoActual = new ShowContestResults(ptrRom, offsetScript);
						break;
					case ContestLinkTransfer.ID:
						comandoActual = new ContestLinkTransfer(ptrRom, offsetScript);
						break;
					case PokemonGBAFrameWork.ComandosScript.Random.ID:
						comandoActual = new PokemonGBAFrameWork.ComandosScript.Random(ptrRom, offsetScript);
						break;
				//estos me los salto
				//falta añadir asta CRY incluido
					case GiveMoney.ID:
						comandoActual = new GiveMoney(ptrRom, offsetScript);
						break;
					case PayMoney.ID:
						comandoActual = new PayMoney(ptrRom, offsetScript);
						break;
					case CheckMoney.ID:
						comandoActual = new CheckMoney(ptrRom, offsetScript);
						break;
					case ShowMoney.ID:
						comandoActual = new ShowMoney(ptrRom, offsetScript);
						break;
					case HideMoney.ID:
						comandoActual = new HideMoney(ptrRom, offsetScript);
						break;
					case UpdateMoney.ID:
						comandoActual = new UpdateMoney(ptrRom, offsetScript);
						break;
					case Cmd96.ID:
						comandoActual = new Cmd96(ptrRom, offsetScript);
						break;
					case FadeScreen.ID:
						comandoActual = new FadeScreen(ptrRom, offsetScript); 
						break;
					case FadeScreenDelay.ID:
						comandoActual = new FadeScreenDelay(ptrRom, offsetScript);
						break;
					case Darken.ID:
						comandoActual = new Darken(ptrRom, offsetScript);
						break;
					case Lighten.ID:
						comandoActual = new Lighten(ptrRom, offsetScript);
						break;
					case PrepareMsg2.ID:
						comandoActual = new PrepareMsg2(ptrRom, offsetScript);
						break;
					case DoAnimation.ID:
						comandoActual = new DoAnimation(ptrRom, offsetScript);
						break;
					case SetAnimation.ID:
						comandoActual = new SetAnimation(ptrRom, offsetScript);
						break;
					case CheckAnimation.ID:
						comandoActual = new ContestLinkTransfer(ptrRom, offsetScript);
						break;
					case SetHealingPlace.ID:
						comandoActual = new SetHealingPlace(ptrRom, offsetScript);
						break;
					case CheckGender.ID:
						comandoActual = new CheckGender(ptrRom, offsetScript);
						break;
					case PokemonGBAFrameWork.ComandosScript.Cry.ID:
						comandoActual = new PokemonGBAFrameWork.ComandosScript.Cry(ptrRom, offsetScript);
						break;
						
						case SetMapTile.ID:
						comandoActual = new SetMapTile(ptrRom, offsetScript);
						break;
						case ResetWeather.ID:
						comandoActual = new ResetWeather(ptrRom, offsetScript);
						break;
						case SetWeather.ID:
						comandoActual = new SetWeather(ptrRom, offsetScript);
						break;
						case DoWeather.ID:
						comandoActual = new DoWeather(ptrRom, offsetScript);
						break;
						case CmdA6.ID:
						comandoActual = new CmdA6(ptrRom, offsetScript);
						break;
						case SetMapFooter.ID:
						comandoActual = new SetMapFooter(ptrRom, offsetScript);
						break;
						case SpriteLevelUp.ID:
						comandoActual = new SpriteLevelUp(ptrRom, offsetScript);
						break;
						case RestoreSpriteLevel.ID:
						comandoActual = new RestoreSpriteLevel(ptrRom, offsetScript);
						break;
						case CreateSprite.ID:
						comandoActual = new CreateSprite(ptrRom, offsetScript);
						break;
						case SpriteFace2.ID:
						comandoActual = new SpriteFace2(ptrRom, offsetScript);
						break;
						case SetDoorOpened.ID:
						comandoActual = new SetDoorOpened(ptrRom, offsetScript);
						break;
						case SetDoorClosed.ID:
						comandoActual = new SetDoorClosed(ptrRom, offsetScript);
						break;
						case DoorChange.ID:
						comandoActual = new DoorChange(ptrRom, offsetScript);
						break;
						case SetDoorOpened2.ID:
						comandoActual = new SetDoorOpened2(ptrRom, offsetScript);
						break;
						case CmdB1.ID:
						comandoActual = new CmdB1(ptrRom, offsetScript);
						break;
						case CmdB2.ID:
						comandoActual = new CmdB2(ptrRom, offsetScript);
						break;
						case CheckCoins.ID:
						comandoActual = new CheckCoins(ptrRom, offsetScript);
						break;
						case GiveCoins.ID:
						comandoActual = new GiveCoins(ptrRom, offsetScript);
						break;
						case RemoveCoins.ID:
						comandoActual = new RemoveCoins(ptrRom, offsetScript);
						break;
						case SetWildBattle.ID:
						comandoActual = new SetWildBattle(ptrRom, offsetScript);
						break;
						case DoWildBattle.ID:
						comandoActual = new DoWildBattle(ptrRom, offsetScript);
						break;
						case SetVirtualAddress.ID:
						comandoActual = new SetVirtualAddress(ptrRom, offsetScript);
						break;
						case VirtualGoto.ID:
						comandoActual = new VirtualGoto(ptrRom, offsetScript);
						break;
						case VirtualCall.ID:
						comandoActual = new VirtualCall(ptrRom, offsetScript);
						break;
						case VirtualGotoIf.ID:
						comandoActual = new VirtualGotoIf(ptrRom, offsetScript);
						break;
						case VirtualCallIf.ID:
						comandoActual = new VirtualCallIf(ptrRom, offsetScript);
						break;
						case VirtualMsgBox.ID:
						comandoActual = new VirtualMsgBox(ptrRom, offsetScript);
						break;
						case VirtualLoadPointer.ID:
						comandoActual = new VirtualLoadPointer(ptrRom, offsetScript);
						break;
						case VirtualBuffer.ID:
						comandoActual = new VirtualBuffer(ptrRom, offsetScript);
						break;
						case ShowCoins.ID:
						comandoActual = new ShowCoins(ptrRom, offsetScript);
						break;
						case HideCoins.ID:
						comandoActual = new HideCoins(ptrRom, offsetScript);
						break;
						case UpdateCoins.ID:
						comandoActual = new UpdateCoins(ptrRom, offsetScript);
						break;
						case CmdC3.ID:
						comandoActual = new CmdC3(ptrRom, offsetScript);
						break;
						case Warp6.ID:
						comandoActual = new Warp6(ptrRom, offsetScript);
						break;
						case WaitCry.ID:
						comandoActual = new WaitCry(ptrRom, offsetScript);
						break;
						case BufferBoxName.ID:
						comandoActual = new BufferBoxName(ptrRom, offsetScript);
						break;
						case TextColor.ID:
						comandoActual = new TextColor(ptrRom, offsetScript);
						break;
						case CmdC8.ID:
						comandoActual = new CmdC8(ptrRom, offsetScript);
						break;
						case CmdC9.ID:
						comandoActual = new CmdC9(ptrRom, offsetScript);
						break;
						case SignMsg.ID:
						comandoActual = new SignMsg(ptrRom, offsetScript);
						break;
						case NormalMsg.ID:
						comandoActual = new NormalMsg(ptrRom, offsetScript);
						break;
						case CompareHiddenVar.ID:
						comandoActual = new CompareHiddenVar(ptrRom, offsetScript);
						break;
						case SetOvedience.ID:
						comandoActual = new SetOvedience(ptrRom, offsetScript);
						break;
						case CheckObedience.ID:
						comandoActual = new CheckObedience(ptrRom, offsetScript);
						break;
						case ExecuteRam.ID:
						comandoActual = new ExecuteRam(ptrRom, offsetScript);
						break;
						case SetWorldMapFlag.ID:
						comandoActual = new SetWorldMapFlag(ptrRom, offsetScript);
						break;
						case WarpTeleport2.ID:
						comandoActual = new WarpTeleport2(ptrRom, offsetScript);
						break;
						case SetCatchLocation.ID:
						comandoActual = new SetCatchLocation(ptrRom, offsetScript);
						break;
						case Braille2.ID:
						comandoActual = new Braille2(ptrRom, offsetScript);
						break;
						case BufferItems.ID:
						comandoActual = new BufferItems(ptrRom, offsetScript);
						break;
						case CmdD5.ID:
						comandoActual = new CmdD5(ptrRom, offsetScript);
						break;
						case CmdD6.ID:
						comandoActual = new CmdD6(ptrRom, offsetScript);
						break;
						case Warp7.ID:
						comandoActual = new Warp7(ptrRom, offsetScript);
						break;
						case CmdD8.ID:
						comandoActual = new CmdD8(ptrRom, offsetScript);
						break;
						case CmdD9.ID:
						comandoActual = new CmdD9(ptrRom, offsetScript);
						break;
						case HideBox2.ID:
						comandoActual = new HideBox2(ptrRom, offsetScript);
						break;
						case PrepareMsg3.ID:
						comandoActual = new PrepareMsg3(ptrRom, offsetScript);
						break;
						case FadeScreen3.ID:
						comandoActual = new FadeScreen3(ptrRom, offsetScript);
						break;
						case BufferTrainerClass.ID:
						comandoActual = new BufferTrainerClass(ptrRom, offsetScript);
						break;
						case BufferTrainerName.ID:
						comandoActual = new BufferTrainerName(ptrRom, offsetScript);
						break;
						case PokenavCall.ID:
						comandoActual = new PokenavCall(ptrRom, offsetScript);
						break;
						case Warp8.ID:
						comandoActual = new Warp8(ptrRom, offsetScript);
						break;
						case BufferContestType.ID:
						comandoActual = new BufferContestType(ptrRom, offsetScript);
						break;
						case BufferItems2.ID:
						comandoActual = new BufferItems2(ptrRom, offsetScript);
						break;

						//falta revisarlos y poner sus posibles opciones :) y con el tiempo acabar de hacerlo cómodo :)
					case RETURN:
					case END:
						break;
				//si no esta hago una excepcion
					default:
						throw new ScriptMalFormadoException();
				}
				if (comandoActual != null) {
					comandosScript.Add(comandoActual);
					offsetScript += comandoActual.Size;
					offsetScript--;//resto el comando porque ya lo sumo antes
				}
				
			} while(byteComandoActual != END && byteComandoActual != RETURN);
		}
		

		public Llista<Comando> ComandosScript {
			get {
				return comandosScript;
			}
		}

		#region IBloqueConNombre implementation
		public string NombreBloque {
			get {
				if (nombreBloque == null) {
					nombreBloque = "script" + DateTime.Now.Ticks;
				}
				return nombreBloque;
			}
			set {
				nombreBloque = value;
			}
		}
		#endregion
		public void SetScript(RomGba rom, int offset = -1, bool lastComandIsEnd = true)
		{
			byte[] byteDeclaracion = GetDeclaracion(rom, lastComandIsEnd);
			if (offset < 0)
				rom.Data.SearchEmptyBytes(byteDeclaracion.Length);
			if (offset < 0)
				throw new RomSinEspacioException();
			rom.Data.SetArray(offset, byteDeclaracion);

		}
		/// <summary>
		/// Obtiene el script en formato XSE
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="idEnd"></param>
		/// <param name="etiqueta"></param>
		/// <returns></returns>
		public string GetDeclaracionXSE(bool isEnd = false, string etiqueta = "Start")
		{
			
			if (etiqueta == null)
				throw new ArgumentNullException("etiqueta");
			
			StringBuilder strSCript = new StringBuilder();
			strSCript.Append("#dynamic ");
			strSCript.Append(OffsetInicioDynamic.ByteString);
			strSCript.Append(ENTER);
			strSCript.Append("#org @");
			strSCript.Append(etiqueta);
			for (int i = 0; i < ComandosScript.Count; i++) {
				strSCript.Append(ENTER);
				strSCript.Append(ComandosScript[i].LineaEjecucionXSE);
			}
			strSCript.Append(ENTER);
			if (isEnd)
				strSCript.Append("end");
			else
				strSCript.Append("return");
			
			return strSCript.ToString();
			
		}
		public byte[] GetDeclaracion(RomGba rom, params object[] parametros)
		{
			int sizeTotal = 1;//el utimo byte
			int offset = 0;
			byte[] bytesDeclaracion;
			bool isEnd = parametros.Length == 0 ? false : (bool)parametros[0];
			IDeclaracion comandoHaDeclarar;
			int offsetDeclaracion = 0;
			byte[] bytesDeclaracionAux;
			for (int i = 0; i < comandosScript.Count; i++)
				sizeTotal += comandosScript[i].Size;
			bytesDeclaracion = new byte[sizeTotal];

			for (int i = 0; i < comandosScript.Count; i++) {
				comandoHaDeclarar = comandosScript[i] as IDeclaracion;
				if (comandoHaDeclarar != null) {//si se tiene que insertar los bytes en la rom para obtener el offset para la declaracion la inserto y listo
					bytesDeclaracionAux = comandoHaDeclarar.GetDeclaracion(rom);
					offsetDeclaracion = rom.Data.SearchArray(bytesDeclaracionAux);
					if (offsetDeclaracion < 0)
						offsetDeclaracion = rom.Data.SetArray(bytesDeclaracionAux);
				}
				bytesDeclaracion.SetArray(offset, comandosScript[i].GetComandoArray(offsetDeclaracion));
				
				offset += comandosScript[i].Size;
			}
			
			if (isEnd)
				bytesDeclaracion[offset] = END;
			else
				bytesDeclaracion[offset] = RETURN;
			
			return bytesDeclaracion;
		}
		
		public static IList<Script> FromXSE(string pathArchivoXSE)
		{
			if(!System.IO.File.Exists(pathArchivoXSE))
				throw new System.IO.FileNotFoundException("No se ha podido encontrar el archivo...");
			return FromXSE(System.IO.File.ReadAllLines(pathArchivoXSE));
		}
		public static IList<Script> FromXSE(IList<string> scriptXSE)
		{
			if(scriptXSE==null)
				throw new ArgumentNullException("scriptXSE");
			//quitar el dinamic
			//tener en cuenta los define...
			//quitar lineas en blanco
			//anidar scripts anidados
			SortedList<string,Script> dicScriptsCargados=new SortedList<string, Script>();
			//por hacer...
			return dicScriptsCargados.Values;
		}
	}
}
