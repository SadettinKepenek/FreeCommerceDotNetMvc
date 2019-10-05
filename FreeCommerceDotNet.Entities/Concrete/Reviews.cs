﻿using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

public class Reviews:IEntity
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public string Date { get; set; }
    public string Title { get; set; }
    public bool Status { get; set; }

    public Customer customer{ get; set; }
}