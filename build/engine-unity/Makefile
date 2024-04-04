include ../common.mk

CPPFLAGS = -DHAL_PTR

SRCS = \
	../../src/anime.c \
	../../src/conf.c \
	../../src/ciel.c \
	../../src/event.c \
	../../src/glyph.c \
	../../src/gui.c \
	../../src/halwrap.c \
	../../src/history.c \
	../../src/image.c \
	../../src/log.c \
	../../src/main.c \
	../../src/mixer.c \
	../../src/readimage.c \
	../../src/readpng.c \
	../../src/readjpeg.c \
	../../src/readwebp.c \
	../../src/save.c \
	../../src/scbuf.c \
	../../src/script.c \
	../../src/seen.c \
	../../src/stage.c \
	../../src/vars.c \
	../../src/wave.c \
	../../src/wms_core.c \
	../../src/wms_lexer.yy.c \
	../../src/wms_parser.tab.c \
	../../src/wms_impl.c \
	../../src/cmd_anime.c \
	../../src/cmd_bg.c \
	../../src/cmd_bgm.c \
	../../src/cmd_ch.c \
	../../src/cmd_cha.c \
	../../src/cmd_chapter.c \
	../../src/cmd_chs.c \
	../../src/cmd_click.c \
	../../src/cmd_gosub.c \
	../../src/cmd_goto.c \
	../../src/cmd_gui.c \
	../../src/cmd_if.c \
	../../src/cmd_layer.c \
	../../src/cmd_load.c \
	../../src/cmd_message.c \
	../../src/cmd_pencil.c \
	../../src/cmd_return.c \
	../../src/cmd_se.c \
	../../src/cmd_set.c \
	../../src/cmd_setconfig.c \
	../../src/cmd_setsave.c \
	../../src/cmd_shake.c \
	../../src/cmd_skip.c \
	../../src/cmd_switch.c \
	../../src/cmd_video.c \
	../../src/cmd_vol.c \
	../../src/cmd_wait.c \
	../../src/cmd_wms.c

LIBS_WINDOWS = \
	../engine-windows-64/libroot/lib/libwebp.a \
	../engine-windows-64/libroot/lib/libwebpdemux.a \
	../engine-windows-64/libroot/lib/libbrotlicommon.a \
	../engine-windows-64/libroot/lib/libsharpyuv.a \
	../engine-windows-64/libroot/lib/libfreetype.a \
	../engine-windows-64/libroot/lib/libbrotlidec.a \
	../engine-windows-64/libroot/lib/libpng16.a \
	../engine-windows-64/libroot/lib/libjpeg.a \
	../engine-windows-64/libroot/lib/libvorbisfile.a \
	../engine-windows-64/libroot/lib/libvorbisenc.a \
	../engine-windows-64/libroot/lib/libvorbis.a \
	../engine-windows-64/libroot/lib/libogg.a \
	../engine-windows-64/libroot/lib/libbz2.a \
	../engine-windows-64/libroot/lib/libz.a

LIBS_MACOS = \
	../engine-macos/libroot/lib/libwebp.a \
	../engine-macos/libroot/lib/libwebpdemux.a \
	../engine-macos/libroot/lib/libbrotlicommon.a \
	../engine-macos/libroot/lib/libsharpyuv.a \
	../engine-macos/libroot/lib/libfreetype.a \
	../engine-macos/libroot/lib/libbrotlidec.a \
	../engine-macos/libroot/lib/libpng16.a \
	../engine-macos/libroot/lib/libjpeg.a \
	../engine-macos/libroot/lib/libvorbisfile.a \
	../engine-macos/libroot/lib/libvorbisenc.a \
	../engine-macos/libroot/lib/libvorbis.a \
	../engine-macos/libroot/lib/libogg.a \
	../engine-macos/libroot/lib/libbz2.a \
	../engine-macos/libroot/lib/libz.a

all: macos

macos: ../engine-macos/libroot $(SRCS)
	rm -rf Assets/libpolaris.bundle
	clang -O0 -g3 -arch arm64 -arch x86_64 -dynamiclib -undefined dynamic_lookup -o Assets/libpolaris.dylib $(CPPFLAGS) -I../engine-macos/libroot/include -I../engine-macos/libroot/include/freetype2 $(SRCS) $(LIBS_MACOS)
	rm -rf Assets/libpolaris.dylib.dSYM
	#/Applications/Unity/Hub/Editor/2022.3.23f1/Unity.app/Contents/MacOS/Unity -batchmode -logFile build.log -projectPath ../engine-unity -executeMethod Build.BuildMacOS

windows: ../engine-windows-64/libroot $(SRCS)
	x86_64-w64-mingw32-gcc -O0 -g3 -shared -o Assets/libpolaris.dll $(CPPFLAGS) -I../engine-windows-64/libroot/include -I../engine-windows-64/libroot/include/freetype2 $(SRCS) $(LIBS_WINDOWS)
	#/Applications/Unity/Hub/Editor/2022.3.23f1/Unity.app/Contents/MacOS/Unity -batchmode -logFile build.log -projectPath ../engine-unity -executeMethod Build.BuildWindows

clean:
	@rm -rf Assets/libpolaris.dylib* Assets/libpolaris.so* Assets/libpolaris.dll*
