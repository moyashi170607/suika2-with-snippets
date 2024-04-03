include ../common.mk

CPPFLAGS = \
	-DHAL_PTR \
	-I./libroot/include \
	-I./libroot/include/freetype2

SRCS = \
	$(SRCS_MAIN) \
	../../src/halwrap.c \

OBJS = $(SRCS:../../src/%.c=%.o)

%.o: ../../src/%.c
	$(CC) -c $(CPPFLAGS) $(CFLAGS) $<

LIBS = \
	./libroot/lib/libwebp.a \
	./libroot/lib/libwebpdemux.a \
	./libroot/lib/libbrotlicommon.a \
	./libroot/lib/libsharpyuv.a \
	./libroot/lib/libfreetype.a \
	./libroot/lib/libbrotlidec.a \
	./libroot/lib/libpng16.a \
	./libroot/lib/libjpeg.a \
	./libroot/lib/libvorbisfile.a \
	./libroot/lib/libvorbisenc.a \
	./libroot/lib/libvorbis.a \
	./libroot/lib/libogg.a \
	./libroot/lib/libbz2.a \
	./libroot/lib/libz.a

macos: libroot $(OBJS)
	CC -dynamiclib -o libpolaris.dylib $(OBJS) $(LIBS)

libroot:
	./build-libs.sh

clean:
	@rm -rf libpolaris.a libroot
