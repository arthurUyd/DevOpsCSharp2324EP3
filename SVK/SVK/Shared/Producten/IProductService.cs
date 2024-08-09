﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Producten;

public interface IProductService
{
    Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request);
    Task<ProductDto.Detail> GetDetailAsync(int productId);
    Task<int> CreateAsync(ProductDto.Mutate model);
}