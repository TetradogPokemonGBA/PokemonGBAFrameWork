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
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.ComandosScript;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of Script.
    /// </summary>
    public class Script : IDeclaracion, IBloqueConNombre, ILastResult, IEndScript
    {
        public static readonly Creditos Creditos;
        const string ENTER = "\r\n";
        //enter con el formato correcto// \r\n
        public const byte RETURN = 0x3;
        public const byte END = 0x2;

        public static Hex OffsetInicioDynamic = "800000";
        Llista<Comando> comandosScript;
        string nombreBloque;
        bool? isEndFinished;
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
            unsafe
            {
                fixed (byte* ptBytesScirpt = bytesScript)
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
            //quizas no siempre acaba en end o return y acaba por ejemplo llamando a otra funcion...por testear la solución...
            //obtengo los comandos hasta encontrar return , end o un comando que acabe la función
            //podria ser que un byteComandoActual depende de una edicion u otra llama a una funcion...u otra???sino no me lo explico...
            //RojoFuegoEsp->1657F0 el call que hay llama a un script que contiene comandos de Esmeralda...es raro...
            byte byteComandoActual;
            Comando comandoActual;
            IEndScript endScriptComando = null;
            do
            {
                comandoActual = null;
                byteComandoActual = ptrRom[offsetScript];
                offsetScript++;
                switch (byteComandoActual)
                {
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
                        comandoActual = new SetTrainerFlag(ptrRom, offsetScript);
                        break;
                    case MoveSprite2.ID:
                        comandoActual = new MoveSprite2(ptrRom, offsetScript);
                        break;
                    case MoveOffScreen.ID:
                        comandoActual = new MoveOffScreen(ptrRom, offsetScript);
                        break;
                    case SpriteBehave.ID:
                        comandoActual = new SpriteBehave(ptrRom, offsetScript);
                        break;
                    case WaitMsg.ID:
                        comandoActual = new WaitMsg(ptrRom, offsetScript);
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
                    case RETURN:
                    case END:
                        break;
                    //si no esta hago una excepcion
                    default:
                        throw new ScriptMalFormadoException(new OffsetRom(offsetScript-1));
                }

                if (comandoActual != null)
                {
                    endScriptComando = comandoActual as IEndScript;
                    comandosScript.Add(comandoActual);
                    offsetScript += comandoActual.Size;
                    offsetScript--;//resto el comando porque ya lo sumo antes
                }

            } while (byteComandoActual != END && byteComandoActual != RETURN && endScriptComando == null || !endScriptComando.IsEnd);
            //tiene que ser un campo calculado...que lea el script y luego devuelva el valor...
            isEndFinished = endScriptComando == null ? (byteComandoActual == END) : new Nullable<bool>();//si acaba con un goto/call/comandoIEndScript será null si acaba en end será true y si es return pues false
        }


        public Llista<Comando> ComandosScript
        {
            get
            {
                return comandosScript;
            }
        }


        #region ILastResult implementation
        public IList<object> LastResult
        {
            get
            {
                ILastResult lastResult = null;
                ILastResult aux;
                for (int i = 0; i < comandosScript.Count; i++)
                {
                    aux = comandosScript[i] as ILastResult;
                    if (aux != null)
                        lastResult = aux;
                }
                return lastResult != null ? lastResult.LastResult : new object[0];
            }
        }

        #region IEndScript implementation


        public bool IsEnd
        {
            get
            {
                IEndScript iEnd = null;
                IEndScript aux;
                for (int i = 0; i < comandosScript.Count && iEnd == null; i++)
                {
                    aux = comandosScript[i] as IEndScript;
                    if (aux != null && aux.IsEnd)
                        iEnd = aux;
                }
                return iEnd != null;
            }
        }
        /// <summary>
        /// Es el valor que tiene al leerse de la rom si es null es porque acaba con un comando IEndScript
        /// </summary>
        public bool? IsEndFinished
        {
            get
            {
                return isEndFinished;
            }
        }

        #endregion

        #endregion
        #region IBloqueConNombre implementation
        public string NombreBloque
        {
            get
            {
                if (nombreBloque == null)
                {
                    nombreBloque = "script" + DateTime.Now.Ticks;
                }
                return nombreBloque;
            }
            set
            {
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
        /// <param name="etiqueta"></param>
        /// <param name="idEnd"></param>
        /// <returns></returns>
        public string GetDeclaracionXSE(string etiqueta = "Start", bool isEnd = false, bool addDynamicTag = true)
        {

            if (etiqueta == null)
                throw new ArgumentNullException("etiqueta");

            StringBuilder strSCript = new StringBuilder();
            if (addDynamicTag)
            {
                strSCript.Append("#dynamic ");
                strSCript.Append(OffsetInicioDynamic.ByteString);
                strSCript.Append(ENTER);
            }
            strSCript.Append("#org @");
            strSCript.Append(etiqueta);
            for (int i = 0; i < ComandosScript.Count; i++)
            {
                strSCript.Append(ENTER);
                strSCript.Append(ComandosScript[i].LineaEjecucionXSE);
            }
            strSCript.Append(ENTER);
            if (EsUnaFuncionAcabadaEnEND(ComandosScript[ComandosScript.Count - 1]))
            {
                if (isEnd)
                    strSCript.Append("end");
                else
                    strSCript.Append("return");
            }
            return strSCript.ToString();

        }

        private bool EsUnaFuncionAcabadaEnEND(Comando comando)
        {
            IEndScript comandoEnd = comando as IEndScript;
            return comandoEnd != null && comandoEnd.IsEnd;
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

            for (int i = 0; i < comandosScript.Count; i++)
            {
                comandoHaDeclarar = comandosScript[i] as IDeclaracion;
                if (comandoHaDeclarar != null)
                {//si se tiene que insertar los bytes en la rom para obtener el offset para la declaracion la inserto y listo
                    bytesDeclaracionAux = comandoHaDeclarar.GetDeclaracion(rom);
                    offsetDeclaracion = rom.Data.SearchArray(bytesDeclaracionAux);
                    if (offsetDeclaracion < 0)
                        offsetDeclaracion = rom.Data.SearchEmptySpaceAndSetArray(bytesDeclaracionAux);
                }
                bytesDeclaracion.SetArray(offset, comandosScript[i].GetComandoArray(offsetDeclaracion));

                offset += comandosScript[i].Size;
            }

            if (EsUnaFuncionAcabadaEnEND(ComandosScript[ComandosScript.Count - 1]))
            {
                if (isEnd)
                    bytesDeclaracion[offset] = END;
                else
                    bytesDeclaracion[offset] = RETURN;
            }
            return bytesDeclaracion;
        }

        public static IList<Script> FromXSE(string pathArchivoXSE)
        {
            if (!System.IO.File.Exists(pathArchivoXSE))
                throw new System.IO.FileNotFoundException("No se ha podido encontrar el archivo...");
            return FromXSE(System.IO.File.ReadAllLines(pathArchivoXSE));
        }
        public static IList<Script> FromXSE(IList<string> scriptXSE)
        {//por acabar
            if (scriptXSE == null)
                throw new ArgumentNullException("scriptXSE");
            //quitar el dinamic
            //tener en cuenta los define...
            //quitar lineas en blanco
            //anidar scripts anidados
            SortedList<string, Script> dicScriptsCargados = new SortedList<string, Script>();
            string[] comandoActualCampos;
            Comando comandoActual;
            for (int i = 0; i < scriptXSE.Count; i++)
            {
                if (scriptXSE[i].Contains(" "))
                {
                    comandoActualCampos = scriptXSE[i].ToLower().Split(' ');
                    try
                    {
                        //switch (comandoActualCampos[0])
                        //{
                        //    //cambiarlo a if y poner .ToLower()
                        //    //pongo los comandos
                        //    case Nop.NOMBRE:
                        //        comandoActual = new Nop(comandoActualCampos);
                        //        break;
                        //    case Nop1.NOMBRE:
                        //        comandoActual = new Nop1(comandoActualCampos);
                        //        break;
                        //    case Call.NOMBRE:
                        //        comandoActual = new Call(comandoActualCampos);
                        //        break;
                        //    case Goto.NOMBRE:
                        //        comandoActual = new Goto(comandoActualCampos);
                        //        break;
                        //    case If1.NOMBRE:
                        //        comandoActual = new If1(comandoActualCampos);
                        //        break;
                        //    case If2.NOMBRE:
                        //        comandoActual = new If2(comandoActualCampos);
                        //        break;
                        //    case Gotostd.NOMBRE:
                        //        comandoActual = new Gotostd(comandoActualCampos);
                        //        break;
                        //    case Callstd.NOMBRE:
                        //        comandoActual = new Callstd(comandoActualCampos);
                        //        break;
                        //    case Gotostdif.NOMBRE:
                        //        comandoActual = new Gotostdif(comandoActualCampos);
                        //        break;
                        //    case Callstdif.NOMBRE:
                        //        comandoActual = new Callstdif(comandoActualCampos);
                        //        break;
                        //    case Jumpram.NOMBRE:
                        //        comandoActual = new Jumpram(comandoActualCampos);
                        //        break;
                        //    case Killscript.NOMBRE:
                        //        comandoActual = new Killscript(comandoActualCampos);
                        //        break;
                        //    case SetByte.NOMBRE:
                        //        comandoActual = new SetByte(comandoActualCampos);
                        //        break;
                        //    case LoadPointer.NOMBRE:
                        //        comandoActual = new LoadPointer(comandoActualCampos);
                        //        break;
                        //    case SetByte2.NOMBRE:
                        //        comandoActual = new SetByte2(comandoActualCampos);
                        //        break;
                        //    case WriteByteToOffset.NOMBRE:
                        //        comandoActual = new WriteByteToOffset(comandoActualCampos);
                        //        break;
                        //    case LoadByteFromPointer.NOMBRE:
                        //        comandoActual = new LoadByteFromPointer(comandoActualCampos);
                        //        break;
                        //    case SetFarByte.NOMBRE:
                        //        comandoActual = new SetFarByte(comandoActualCampos);
                        //        break;
                        //    case Copyscriptbanks.NOMBRE.ToLower():
                        //        comandoActual = new Copyscriptbanks(comandoActualCampos);
                        //        break;
                        //    case CopyByte.NOMBRE:
                        //        comandoActual = new CopyByte(comandoActualCampos);
                        //        break;
                        //    case SetVar.NOMBRE:
                        //        comandoActual = new SetVar(comandoActualCampos);
                        //        break;
                        //    case AddVar.NOMBRE:
                        //        comandoActual = new AddVar(comandoActualCampos);
                        //        break;
                        //    case SubVar.NOMBRE:
                        //        comandoActual = new SubVar(comandoActualCampos);
                        //        break;
                        //    case CopyVar.NOMBRE:
                        //        comandoActual = new CopyVar(comandoActualCampos);
                        //        break;
                        //    case CopyVarIfNotZero.NOMBRE:
                        //        comandoActual = new CopyVarIfNotZero(comandoActualCampos);
                        //        break;
                        //    case CompareBanks.NOMBRE:
                        //        comandoActual = new CompareBanks(comandoActualCampos);
                        //        break;
                        //    case CompareBankToByte.NOMBRE:
                        //        comandoActual = new CompareBankToByte(comandoActualCampos);
                        //        break;
                        //    case CompareBankToFarByte.NOMBRE:
                        //        comandoActual = new CompareBankToFarByte(comandoActualCampos);
                        //        break;
                        //    case CompareFarByteToBank.NOMBRE:
                        //        comandoActual = new CompareBankToFarByte(comandoActualCampos);
                        //        break;
                        //    case CompareFarByteToByte.NOMBRE:
                        //        comandoActual = new CompareFarByteToByte(comandoActualCampos);
                        //        break;
                        //    case CompareFarBytes.NOMBRE:
                        //        comandoActual = new CompareFarBytes(comandoActualCampos);
                        //        break;
                        //    case Compare.NOMBRE:
                        //        comandoActual = new Compare(comandoActualCampos);
                        //        break;
                        //    case CompareVars.NOMBRE:
                        //        comandoActual = new CompareVars(comandoActualCampos);
                        //        break;
                        //    case CallAsm.NOMBRE:
                        //        comandoActual = new CallAsm(comandoActualCampos);
                        //        break;
                        //    case Cmd24.NOMBRE:
                        //        comandoActual = new Cmd24(comandoActualCampos);
                        //        break;
                        //    case Special.NOMBRE:
                        //        comandoActual = new Special(comandoActualCampos);
                        //        break;
                        //    case Special2.NOMBRE:
                        //        comandoActual = new Special2(comandoActualCampos);
                        //        break;
                        //    case WaitState.NOMBRE:
                        //        comandoActual = new WaitState(comandoActualCampos);
                        //        break;
                        //    case Pause.NOMBRE:
                        //        comandoActual = new Pause(comandoActualCampos);
                        //        break;
                        //    case SetFlag.NOMBRE:
                        //        comandoActual = new SetFlag(comandoActualCampos);
                        //        break;
                        //    case ClearFlag.NOMBRE:
                        //        comandoActual = new ClearFlag(comandoActualCampos);
                        //        break;
                        //    case CheckFlag.NOMBRE:
                        //        comandoActual = new CheckFlag(comandoActualCampos);
                        //        break;
                        //    case Cmd2C.NOMBRE:
                        //        comandoActual = new Cmd2C(comandoActualCampos);
                        //        break;
                        //    case CheckSound.NOMBRE:
                        //        comandoActual = new CheckSound(comandoActualCampos);
                        //        break;
                        //    case FanFare.NOMBRE:
                        //        comandoActual = new FanFare(comandoActualCampos);
                        //        break;
                        //    case WaitFanFare.NOMBRE:
                        //        comandoActual = new WaitFanFare(comandoActualCampos);
                        //        break;
                        //    case PlaySong.NOMBRE:
                        //        comandoActual = new PlaySong(comandoActualCampos);
                        //        break;
                        //    case PlaySong2.NOMBRE:
                        //        comandoActual = new PlaySong2(comandoActualCampos);
                        //        break;
                        //    case FadeDefault.NOMBRE:
                        //        comandoActual = new FadeDefault(comandoActualCampos);
                        //        break;
                        //    case FadeSong.NOMBRE:
                        //        comandoActual = new FadeSong(comandoActualCampos);
                        //        break;
                        //    case FadeOut.NOMBRE:
                        //        comandoActual = new FadeOut(comandoActualCampos);
                        //        break;
                        //    case FadeIn.NOMBRE:
                        //        comandoActual = new FadeIn(comandoActualCampos);
                        //        break;
                        //    case CheckDailyFlags.NOMBRE:
                        //        comandoActual = new CheckDailyFlags(comandoActualCampos);
                        //        break;
                        //    case ResetVars.NOMBRE:
                        //        comandoActual = new ResetVars(comandoActualCampos);
                        //        break;
                        //    case Sound.NOMBRE:
                        //        comandoActual = new Sound(comandoActualCampos);
                        //        break;
                        //    case Warp.NOMBRE:
                        //        comandoActual = new Warp(comandoActualCampos);
                        //        break;
                        //    case WarpMuted.NOMBRE:
                        //        comandoActual = new WarpMuted(comandoActualCampos);
                        //        break;
                        //    case WarpWalk.NOMBRE:
                        //        comandoActual = new WarpWalk(comandoActualCampos);
                        //        break;
                        //    case WarpHole.NOMBRE:
                        //        comandoActual = new WarpHole(comandoActualCampos);
                        //        break;
                        //    case WarpTeleport.NOMBRE:
                        //        comandoActual = new WarpTeleport(comandoActualCampos);
                        //        break;
                        //    case Warp3.NOMBRE:
                        //        comandoActual = new Warp3(comandoActualCampos);
                        //        break;
                        //    case SetWarpplace.NOMBRE:
                        //        comandoActual = new SetWarpplace(comandoActualCampos);
                        //        break;
                        //    case Warp4.NOMBRE:
                        //        comandoActual = new Warp4(comandoActualCampos);
                        //        break;
                        //    case Warp5.NOMBRE:
                        //        comandoActual = new Warp5(comandoActualCampos);
                        //        break;
                        //    case GetPlayerPos.NOMBRE:
                        //        comandoActual = new GetPlayerPos(comandoActualCampos);
                        //        break;
                        //    case CountPokemon.NOMBRE:
                        //        comandoActual = new CountPokemon(comandoActualCampos);
                        //        break;
                        //    case AddItem.NOMBRE:
                        //        comandoActual = new AddItem(comandoActualCampos);
                        //        break;
                        //    case RemoveItem.NOMBRE:
                        //        comandoActual = new RemoveItem(comandoActualCampos);
                        //        break;
                        //    case CheckItemRoom.NOMBRE:
                        //        comandoActual = new CheckItemRoom(comandoActualCampos);
                        //        break;
                        //    case CheckItem.NOMBRE:
                        //        comandoActual = new CheckItem(comandoActualCampos);
                        //        break;
                        //    case CheckItemType.NOMBRE:
                        //        comandoActual = new CheckItemType(comandoActualCampos);
                        //        break;
                        //    case AddPcItem.NOMBRE:
                        //        comandoActual = new AddPcItem(comandoActualCampos);
                        //        break;
                        //    case CheckPcItem.NOMBRE:
                        //        comandoActual = new CheckPcItem(comandoActualCampos);
                        //        break;
                        //    case AddDecoration.NOMBRE:
                        //        comandoActual = new AddDecoration(comandoActualCampos);
                        //        break;
                        //    case RemoveDecoration.NOMBRE:
                        //        comandoActual = new RemoveDecoration(comandoActualCampos);
                        //        break;
                        //    case TestDecoration.NOMBRE:
                        //        comandoActual = new TestDecoration(comandoActualCampos);
                        //        break;
                        //    case CheckDecoration.NOMBRE:
                        //        comandoActual = new CheckDecoration(comandoActualCampos);
                        //        break;
                        //    case ApplyMovement.NOMBRE:
                        //        comandoActual = new ApplyMovement(comandoActualCampos);
                        //        break;
                        //    case ApplyMovementPos.NOMBRE:
                        //        comandoActual = new ApplyMovementPos(comandoActualCampos);
                        //        break;
                        //    case WaitMovement.NOMBRE:
                        //        comandoActual = new WaitMovement(comandoActualCampos);
                        //        break;
                        //    case WaitMovementPos.NOMBRE:
                        //        comandoActual = new WaitMovementPos(comandoActualCampos);
                        //        break;
                        //    case HideSprite.NOMBRE:
                        //        comandoActual = new HNOMBREeSprite(comandoActualCampos);
                        //        break;
                        //    case HideSpritePos.NOMBRE:
                        //        comandoActual = new HNOMBREeSpritePos(comandoActualCampos);
                        //        break;
                        //    case ShowSprite.NOMBRE:
                        //        comandoActual = new ShowSprite(comandoActualCampos);
                        //        break;
                        //    case ShowSpritePos.NOMBRE:
                        //        comandoActual = new ShowSpritePos(comandoActualCampos);
                        //        break;
                        //    case MoveSprite.NOMBRE:
                        //        comandoActual = new MoveSprite(comandoActualCampos);
                        //        break;
                        //    case SpriteVisible.NOMBRE:
                        //        comandoActual = new SpriteVisible(comandoActualCampos);
                        //        break;
                        //    case SpriteInvisible.NOMBRE:
                        //        comandoActual = new SpriteInvisible(comandoActualCampos);
                        //        break;
                        //    case Faceplayer.NOMBRE:
                        //        comandoActual = new Faceplayer(comandoActualCampos);
                        //        break;
                        //    case SpriteFace.NOMBRE:
                        //        comandoActual = new SpriteFace(comandoActualCampos);
                        //        break;
                        //    case Trainerbattle.NOMBRE:
                        //        comandoActual = new Trainerbattle(comandoActualCampos);
                        //        break;
                        //    case RepeatTrainerBattle.NOMBRE:
                        //        comandoActual = new RepeatTrainerBattle(comandoActualCampos);
                        //        break;
                        //    case EndTrainerBattle.NOMBRE:
                        //        comandoActual = new EndTrainerBattle(comandoActualCampos);
                        //        break;
                        //    case EndTrainerBattle2.NOMBRE:
                        //        comandoActual = new EndTrainerBattle2(comandoActualCampos);
                        //        break;
                        //    case CheckTrainerFlag.NOMBRE:
                        //        comandoActual = new CheckTrainerFlag(comandoActualCampos);
                        //        break;
                        //    case ClearTrainerFlag.NOMBRE:
                        //        comandoActual = new ClearTrainerFlag(comandoActualCampos);
                        //        break;
                        //    case SetTrainerFlag.NOMBRE:
                        //        comandoActual = new SetTrainerFlag(comandoActualCampos);
                        //        break;
                        //    case MoveSprite2.NOMBRE:
                        //        comandoActual = new MoveSprite2(comandoActualCampos);
                        //        break;
                        //    case MoveOffScreen.NOMBRE:
                        //        comandoActual = new MoveOffScreen(comandoActualCampos);
                        //        break;
                        //    case SpriteBehave.NOMBRE:
                        //        comandoActual = new SpriteBehave(comandoActualCampos);
                        //        break;
                        //    case WaitMsg.NOMBRE:
                        //        comandoActual = new WaitMsg(comandoActualCampos);
                        //        break;
                        //    case PrepareMsg.NOMBRE:
                        //        comandoActual = new PrepareMsg(comandoActualCampos);
                        //        break;
                        //    case CloseOnKeyPress.NOMBRE:
                        //        comandoActual = new CloseOnKeyPress(comandoActualCampos);
                        //        break;
                        //    case LockAll.NOMBRE:
                        //        comandoActual = new LockAll(comandoActualCampos);
                        //        break;
                        //    case Lock.NOMBRE:
                        //        comandoActual = new Lock(comandoActualCampos);
                        //        break;
                        //    case ReleaseAll.NOMBRE:
                        //        comandoActual = new ReleaseAll(comandoActualCampos);
                        //        break;
                        //    case Release.NOMBRE:
                        //        comandoActual = new Release(comandoActualCampos);
                        //        break;
                        //    case WaitKeyPress.NOMBRE:
                        //        comandoActual = new WaitKeyPress(comandoActualCampos);
                        //        break;
                        //    case YesNoBox.NOMBRE:
                        //        comandoActual = new YesNoBox(comandoActualCampos);
                        //        break;
                        //    case Multichoice.NOMBRE:
                        //        comandoActual = new Multichoice(comandoActualCampos);
                        //        break;
                        //    case Multichoice2.NOMBRE:
                        //        comandoActual = new Multichoice2(comandoActualCampos);
                        //        break;
                        //    case Multichoice3.NOMBRE:
                        //        comandoActual = new Multichoice3(comandoActualCampos);
                        //        break;
                        //    case ShowBox.NOMBRE:
                        //        comandoActual = new ShowBox(comandoActualCampos);
                        //        break;
                        //    case HideBox.NOMBRE:
                        //        comandoActual = new HideBox(comandoActualCampos);
                        //        break;
                        //    case ClearBox.NOMBRE:
                        //        comandoActual = new ClearBox(comandoActualCampos);
                        //        break;
                        //    case ShowPokePic.NOMBRE:
                        //        comandoActual = new ShowPokePic(comandoActualCampos);
                        //        break;
                        //    case HidePokePic.NOMBRE:
                        //        comandoActual = new HidePokePic(comandoActualCampos);
                        //        break;
                        //    case ShowContestWinner.NOMBRE:
                        //        comandoActual = new ShowContestWinner(comandoActualCampos);
                        //        break;
                        //    case Braille.NOMBRE:
                        //        comandoActual = new Braille(comandoActualCampos);
                        //        break;
                        //    case GivePokemon.NOMBRE:
                        //        comandoActual = new GivePokemon(comandoActualCampos);
                        //        break;
                        //    case GiveEgg.NOMBRE:
                        //        comandoActual = new GiveEgg(comandoActualCampos);
                        //        break;
                        //    case SetPkmnPP.NOMBRE:
                        //        comandoActual = new SetPkmnPP(comandoActualCampos);
                        //        break;
                        //    case CheckAttack.NOMBRE:
                        //        comandoActual = new CheckAttack(comandoActualCampos);
                        //        break;
                        //    case BufferPokemon.NOMBRE:
                        //        comandoActual = new BufferPokemon(comandoActualCampos);
                        //        break;
                        //    case BufferFirstPokemon.NOMBRE:
                        //        comandoActual = new BufferFirstPokemon(comandoActualCampos);
                        //        break;
                        //    case BufferPartyPokemon.NOMBRE:
                        //        comandoActual = new BufferPartyPokemon(comandoActualCampos);
                        //        break;
                        //    case BufferItem.NOMBRE:
                        //        comandoActual = new BufferItem(comandoActualCampos);
                        //        break;
                        //    case BufferDecoration.NOMBRE:
                        //        comandoActual = new BufferDecoration(comandoActualCampos);
                        //        break;
                        //    case BufferAttack.NOMBRE:
                        //        comandoActual = new BufferAttack(comandoActualCampos);
                        //        break;
                        //    case BufferNumber.NOMBRE:
                        //        comandoActual = new BufferNumber(comandoActualCampos);
                        //        break;
                        //    case BufferStd.NOMBRE:
                        //        comandoActual = new BufferStd(comandoActualCampos);
                        //        break;
                        //    case BufferString.NOMBRE:
                        //        comandoActual = new BufferString(comandoActualCampos);
                        //        break;
                        //    case PokeMart.NOMBRE:
                        //        comandoActual = new PokeMart(comandoActualCampos);
                        //        break;
                        //    case PokeMart2.NOMBRE:
                        //        comandoActual = new PokeMart2(comandoActualCampos);
                        //        break;
                        //    case PokeMart3.NOMBRE:
                        //        comandoActual = new PokeMart3(comandoActualCampos);
                        //        break;
                        //    case PokeCasino.NOMBRE:
                        //        comandoActual = new PokeCasino(comandoActualCampos);
                        //        break;
                        //    case Cmd8A.NOMBRE:
                        //        comandoActual = new Cmd8A(comandoActualCampos);
                        //        break;
                        //    case ChooseContestPkmn.NOMBRE:
                        //        comandoActual = new ChooseContestPkmn(comandoActualCampos);
                        //        break;
                        //    case StartContest.NOMBRE:
                        //        comandoActual = new StartContest(comandoActualCampos);
                        //        break;
                        //    case ShowContestResults.NOMBRE:
                        //        comandoActual = new ShowContestResults(comandoActualCampos);
                        //        break;
                        //    case ContestLinkTransfer.NOMBRE:
                        //        comandoActual = new ContestLinkTransfer(comandoActualCampos);
                        //        break;
                        //    case PokemonGBAFrameWork.ComandosScript.Random.NOMBRE:
                        //        comandoActual = new PokemonGBAFrameWork.ComandosScript.Random(comandoActualCampos);
                        //        break;
                        //    //estos me los salto
                        //    //falta añadir asta CRY incluido
                        //    case GiveMoney.NOMBRE:
                        //        comandoActual = new GiveMoney(comandoActualCampos);
                        //        break;
                        //    case PayMoney.NOMBRE:
                        //        comandoActual = new PayMoney(comandoActualCampos);
                        //        break;
                        //    case CheckMoney.NOMBRE:
                        //        comandoActual = new CheckMoney(comandoActualCampos);
                        //        break;
                        //    case ShowMoney.NOMBRE:
                        //        comandoActual = new ShowMoney(comandoActualCampos);
                        //        break;
                        //    case HideMoney.NOMBRE:
                        //        comandoActual = new HideMoney(comandoActualCampos);
                        //        break;
                        //    case UpdateMoney.NOMBRE:
                        //        comandoActual = new UpdateMoney(comandoActualCampos);
                        //        break;
                        //    case Cmd96.NOMBRE:
                        //        comandoActual = new Cmd96(comandoActualCampos);
                        //        break;
                        //    case FadeScreen.NOMBRE:
                        //        comandoActual = new FadeScreen(comandoActualCampos);
                        //        break;
                        //    case FadeScreenDelay.NOMBRE:
                        //        comandoActual = new FadeScreenDelay(comandoActualCampos);
                        //        break;
                        //    case Darken.NOMBRE:
                        //        comandoActual = new Darken(comandoActualCampos);
                        //        break;
                        //    case Lighten.NOMBRE:
                        //        comandoActual = new Lighten(comandoActualCampos);
                        //        break;
                        //    case PrepareMsg2.NOMBRE:
                        //        comandoActual = new PrepareMsg2(comandoActualCampos);
                        //        break;
                        //    case DoAnimation.NOMBRE:
                        //        comandoActual = new DoAnimation(comandoActualCampos);
                        //        break;
                        //    case SetAnimation.NOMBRE:
                        //        comandoActual = new SetAnimation(comandoActualCampos);
                        //        break;
                        //    case CheckAnimation.NOMBRE:
                        //        comandoActual = new ContestLinkTransfer(comandoActualCampos);
                        //        break;
                        //    case SetHealingPlace.NOMBRE:
                        //        comandoActual = new SetHealingPlace(comandoActualCampos);
                        //        break;
                        //    case CheckGender.NOMBRE:
                        //        comandoActual = new CheckGender(comandoActualCampos);
                        //        break;
                        //    case PokemonGBAFrameWork.ComandosScript.Cry.NOMBRE:
                        //        comandoActual = new PokemonGBAFrameWork.ComandosScript.Cry(comandoActualCampos);
                        //        break;

                        //    case SetMapTile.NOMBRE:
                        //        comandoActual = new SetMapTile(comandoActualCampos);
                        //        break;
                        //    case ResetWeather.NOMBRE:
                        //        comandoActual = new ResetWeather(comandoActualCampos);
                        //        break;
                        //    case SetWeather.NOMBRE:
                        //        comandoActual = new SetWeather(comandoActualCampos);
                        //        break;
                        //    case DoWeather.NOMBRE:
                        //        comandoActual = new DoWeather(comandoActualCampos);
                        //        break;
                        //    case CmdA6.NOMBRE:
                        //        comandoActual = new CmdA6(comandoActualCampos);
                        //        break;
                        //    case SetMapFooter.NOMBRE:
                        //        comandoActual = new SetMapFooter(comandoActualCampos);
                        //        break;
                        //    case SpriteLevelUp.NOMBRE:
                        //        comandoActual = new SpriteLevelUp(comandoActualCampos);
                        //        break;
                        //    case RestoreSpriteLevel.NOMBRE:
                        //        comandoActual = new RestoreSpriteLevel(comandoActualCampos);
                        //        break;
                        //    case CreateSprite.NOMBRE:
                        //        comandoActual = new CreateSprite(comandoActualCampos);
                        //        break;
                        //    case SpriteFace2.NOMBRE:
                        //        comandoActual = new SpriteFace2(comandoActualCampos);
                        //        break;
                        //    case SetDoorOpened.NOMBRE:
                        //        comandoActual = new SetDoorOpened(comandoActualCampos);
                        //        break;
                        //    case SetDoorClosed.NOMBRE:
                        //        comandoActual = new SetDoorClosed(comandoActualCampos);
                        //        break;
                        //    case DoorChange.NOMBRE:
                        //        comandoActual = new DoorChange(comandoActualCampos);
                        //        break;
                        //    case SetDoorOpened2.NOMBRE:
                        //        comandoActual = new SetDoorOpened2(comandoActualCampos);
                        //        break;
                        //    case CmdB1.NOMBRE:
                        //        comandoActual = new CmdB1(comandoActualCampos);
                        //        break;
                        //    case CmdB2.NOMBRE:
                        //        comandoActual = new CmdB2(comandoActualCampos);
                        //        break;
                        //    case CheckCoins.NOMBRE:
                        //        comandoActual = new CheckCoins(comandoActualCampos);
                        //        break;
                        //    case GiveCoins.NOMBRE:
                        //        comandoActual = new GiveCoins(comandoActualCampos);
                        //        break;
                        //    case RemoveCoins.NOMBRE:
                        //        comandoActual = new RemoveCoins(comandoActualCampos);
                        //        break;
                        //    case SetWildBattle.NOMBRE:
                        //        comandoActual = new SetWildBattle(comandoActualCampos);
                        //        break;
                        //    case DoWildBattle.NOMBRE:
                        //        comandoActual = new DoWildBattle(comandoActualCampos);
                        //        break;
                        //    case SetVirtualAddress.NOMBRE:
                        //        comandoActual = new SetVirtualAddress(comandoActualCampos);
                        //        break;
                        //    case VirtualGoto.NOMBRE:
                        //        comandoActual = new VirtualGoto(comandoActualCampos);
                        //        break;
                        //    case VirtualCall.NOMBRE:
                        //        comandoActual = new VirtualCall(comandoActualCampos);
                        //        break;
                        //    case VirtualGotoIf.NOMBRE:
                        //        comandoActual = new VirtualGotoIf(comandoActualCampos);
                        //        break;
                        //    case VirtualCallIf.NOMBRE:
                        //        comandoActual = new VirtualCallIf(comandoActualCampos);
                        //        break;
                        //    case VirtualMsgBox.NOMBRE:
                        //        comandoActual = new VirtualMsgBox(comandoActualCampos);
                        //        break;
                        //    case VirtualLoadPointer.NOMBRE:
                        //        comandoActual = new VirtualLoadPointer(comandoActualCampos);
                        //        break;
                        //    case VirtualBuffer.NOMBRE:
                        //        comandoActual = new VirtualBuffer(comandoActualCampos);
                        //        break;
                        //    case ShowCoins.NOMBRE:
                        //        comandoActual = new ShowCoins(comandoActualCampos);
                        //        break;
                        //    case HideCoins.NOMBRE:
                        //        comandoActual = new HideCoins(comandoActualCampos);
                        //        break;
                        //    case UpdateCoins.NOMBRE:
                        //        comandoActual = new UpdateCoins(comandoActualCampos);
                        //        break;
                        //    case CmdC3.NOMBRE:
                        //        comandoActual = new CmdC3(comandoActualCampos);
                        //        break;
                        //    case Warp6.NOMBRE:
                        //        comandoActual = new Warp6(comandoActualCampos);
                        //        break;
                        //    case WaitCry.NOMBRE:
                        //        comandoActual = new WaitCry(comandoActualCampos);
                        //        break;
                        //    case BufferBoxName.NOMBRE:
                        //        comandoActual = new BufferBoxName(comandoActualCampos);
                        //        break;
                        //    case TextColor.NOMBRE:
                        //        comandoActual = new TextColor(comandoActualCampos);
                        //        break;
                        //    case CmdC8.NOMBRE:
                        //        comandoActual = new CmdC8(comandoActualCampos);
                        //        break;
                        //    case CmdC9.NOMBRE:
                        //        comandoActual = new CmdC9(comandoActualCampos);
                        //        break;
                        //    case SignMsg.NOMBRE:
                        //        comandoActual = new SignMsg(comandoActualCampos);
                        //        break;
                        //    case NormalMsg.NOMBRE:
                        //        comandoActual = new NormalMsg(comandoActualCampos);
                        //        break;
                        //    case CompareHiddenVar.NOMBRE:
                        //        comandoActual = new CompareHiddenVar(comandoActualCampos);
                        //        break;
                        //    case SetOvedience.NOMBRE:
                        //        comandoActual = new SetOvedience(comandoActualCampos);
                        //        break;
                        //    case CheckObedience.NOMBRE:
                        //        comandoActual = new CheckObedience(comandoActualCampos);
                        //        break;
                        //    case ExecuteRam.NOMBRE:
                        //        comandoActual = new ExecuteRam(comandoActualCampos);
                        //        break;
                        //    case SetWorldMapFlag.NOMBRE:
                        //        comandoActual = new SetWorldMapFlag(comandoActualCampos);
                        //        break;
                        //    case WarpTeleport2.NOMBRE:
                        //        comandoActual = new WarpTeleport2(comandoActualCampos);
                        //        break;
                        //    case SetCatchLocation.NOMBRE:
                        //        comandoActual = new SetCatchLocation(comandoActualCampos);
                        //        break;
                        //    case Braille2.NOMBRE:
                        //        comandoActual = new Braille2(comandoActualCampos);
                        //        break;
                        //    case BufferItems.NOMBRE:
                        //        comandoActual = new BufferItems(comandoActualCampos);
                        //        break;
                        //    case CmdD5.NOMBRE:
                        //        comandoActual = new CmdD5(comandoActualCampos);
                        //        break;
                        //    case CmdD6.NOMBRE:
                        //        comandoActual = new CmdD6(comandoActualCampos);
                        //        break;
                        //    case Warp7.NOMBRE:
                        //        comandoActual = new Warp7(comandoActualCampos);
                        //        break;
                        //    case CmdD8.NOMBRE:
                        //        comandoActual = new CmdD8(comandoActualCampos);
                        //        break;
                        //    case CmdD9.NOMBRE:
                        //        comandoActual = new CmdD9(comandoActualCampos);
                        //        break;
                        //    case HideBox2.NOMBRE:
                        //        comandoActual = new HideBox2(comandoActualCampos);
                        //        break;
                        //    case PrepareMsg3.NOMBRE:
                        //        comandoActual = new PrepareMsg3(comandoActualCampos);
                        //        break;
                        //    case FadeScreen3.NOMBRE:
                        //        comandoActual = new FadeScreen3(comandoActualCampos);
                        //        break;
                        //    case BufferTrainerClass.NOMBRE:
                        //        comandoActual = new BufferTrainerClass(comandoActualCampos);
                        //        break;
                        //    case BufferTrainerName.NOMBRE:
                        //        comandoActual = new BufferTrainerName(comandoActualCampos);
                        //        break;
                        //    case PokenavCall.NOMBRE:
                        //        comandoActual = new PokenavCall(comandoActualCampos);
                        //        break;
                        //    case Warp8.NOMBRE:
                        //        comandoActual = new Warp8(comandoActualCampos);
                        //        break;
                        //    case BufferContestType.NOMBRE:
                        //        comandoActual = new BufferContestType(comandoActualCampos);
                        //        break;
                        //    case BufferItems2.NOMBRE:
                        //        comandoActual = new BufferItems2(comandoActualCampos);
                        //        break;
                        //    case "return":

                        //    case "end":
                        //        break;
                        //    //si no esta hago una excepcion
                        //    default:
                        //        throw new ScriptMalFormadoException();
                        //        //voy por copyvar en poner nombre falta hacer constructor en cada uno
                        //}
                    }
                    catch
                    {
                        //lanzo una excepción diciendo que la linea tal tiene un error
                        throw new ScriptMalFormadoException(i);
                    }
                    //por acabar
                }
            }

            return dicScriptsCargados.Values;
        }
    }
  
}
