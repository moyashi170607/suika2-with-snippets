/* -*- coding: utf-8; indent-tabs-mode: nil; tab-width: 4; c-basic-offset: 4; -*- */

/*
 * Suika2 / Polaris Engine
 * Copyright (C) 2001-2016, Keiichi Tabata. All rights reserved.
 */

/*
 * Audio Unit ミキサ
 *
 * [Changes]
 *  - 2016/06/17 作成
 *  - 2016/07/03 ミキシング実装
 */

#ifndef SUIKA_CAUDIO_H
#define SUIKA_CAUDIO_H

bool init_aunit(void);
void cleanup_aunit(void);
void pause_sound(void);
void resume_sound(void);

#endif
